﻿using System;
using System.Collections.Generic;
using GarageLogic;

namespace UI
{
    internal class WheelsForm
    {
        private YesNoForm m_YesNoForm = new YesNoForm();
        private const string k_InputIsNotANumberErrorMessage = "You must enter a number";
        private const string k_NoLicensePlateCreatedErrorMessage = "License plate was not created yet";
        private string m_ManufacturerName = String.Empty;
        private float m_CurrentTirePressure;
        private List<Vehicle.Wheel> m_Result = null;

        public List<Vehicle.Wheel> Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_NoLicensePlateCreatedErrorMessage);
                }

                return m_Result;
            }
        }

        public void Display(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            bool validFloatEntered = false;
            int counterSuccessfullyLoadedWheels = 0;
            m_Result = new List<Vehicle.Wheel>(i_VehiclePicked.m_WheelsNumber);
            while (counterSuccessfullyLoadedWheels < i_VehiclePicked.m_WheelsNumber)
            {
                try
                {
                    Console.WriteLine(string.Format("Wheel #{0}:", counterSuccessfullyLoadedWheels + 1));
                    Console.WriteLine("Enter manufacturer name of your wheel:");
                    m_ManufacturerName = Console.ReadLine();
                    Console.WriteLine(string.Format("Enter your current tire pressure in your wheels (Maximum is {0}):", i_VehiclePicked.m_MaxTirePressure));
                    validFloatEntered = float.TryParse(Console.ReadLine(), out m_CurrentTirePressure);
                    if (validFloatEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }

                    VehicleUtils.ValidateTirePressureForThisVehicle(m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure);
                    if (counterSuccessfullyLoadedWheels == 0)
                    {
                        bool wantsToApplyAllInfoOnAllWheels = m_YesNoForm.DisplayAndGetResult("Do you want to apply this wheel information to all wheels? (y/n)");
                        if (wantsToApplyAllInfoOnAllWheels == true)
                        {
                            for (int i = 0; i < i_VehiclePicked.m_WheelsNumber; i++)
                            {
                                m_Result.Add(new Vehicle.Wheel(m_ManufacturerName, m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure));
                                counterSuccessfullyLoadedWheels++;
                            }

                            Console.WriteLine(string.Format("Your wheel settings successfuly set up to all {0} wheels", i_VehiclePicked.m_WheelsNumber));
                        }
                        else
                        {
                            m_Result[counterSuccessfullyLoadedWheels] = new Vehicle.Wheel(m_ManufacturerName, m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure);
                        }
                    }
                    else
                    {
                        m_Result[counterSuccessfullyLoadedWheels] = new Vehicle.Wheel(m_ManufacturerName, m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure);
                    }
                    counterSuccessfullyLoadedWheels++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<Vehicle.Wheel> DisplayAndGetResult(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            Display(i_VehiclePicked);
            return m_Result;
        }

        public void ResetForm()
        {
            m_Result = null;
        }
    }
}