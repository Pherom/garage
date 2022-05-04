﻿using System;
using System.Collections.Generic;
using GarageLogic;

namespace UI
{           

    internal class GarageMenuExecutorUI
    {
        private GarageMenuForm m_GarageMenuForm = new GarageMenuForm();
        private Garage m_Garage = new Garage();
        private VehicleFactory m_VehicleFactory = new VehicleFactory();
        private VehicleTypeForm m_VehicleTypeForm = new VehicleTypeForm();
        private LicensePlateForm m_LicensePlateForm = new LicensePlateForm();
        private WheelsForm m_WheelsForm = new WheelsForm();
        private RemainingEnergyOrFuelForm m_RemainingEnergyOrFuelForm = new RemainingEnergyOrFuelForm();
        private SpecificationForm m_SpecificationForm = new SpecificationForm();
        private OwnerDataForm m_OwnerDataForm = new OwnerDataForm();
        private EnumForm m_EnumForm = new EnumForm();

        public void Execute()
        {
            GarageMenuForm.eGarageMenuOption i_OptionPicked;
            bool runningMenu = true;
            while (runningMenu == true)
            {
                i_OptionPicked = m_GarageMenuForm.DisplayAndGetResult();
                switch (i_OptionPicked)
                {
                    case GarageMenuForm.eGarageMenuOption.ADD_NEW_VEHICLE:
                       AddNewVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.DISPLAY_LICENSE_PLATES_OF_ALL_VEHICLES:
                       DisplayLicensePlates();
                       break;
                    case GarageMenuForm.eGarageMenuOption.CHANGE_REPAIR_STATUS_OF_SPECIFIC_VEHICLE:
                       ChangeRepairStatusOfSpecificVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.FUEL_VEHICLE:
                       FuelVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.INFLATE_VEHICLE_TIRES_TO_MAX:
                       InflateVehicleTirexToMax();
                       break;
                    case GarageMenuForm.eGarageMenuOption.CHARGE_ELECTRIC_VEHICLE:
                       ChargeElectricVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.DISPLAY_ALL_INFORMATION_ABOUT_VEHICLE:
                       DisplayAllInfoAboutSpecificVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.EXIT:
                       runningMenu = false;
                       break;
                }

                resetForms();
                Console.WriteLine();
            }
        }

        private void resetForms()
        {
            m_GarageMenuForm.ResetForm();
            m_LicensePlateForm.ResetForm();
            m_VehicleTypeForm.ResetForm();
            m_WheelsForm.ResetForm();
            m_RemainingEnergyOrFuelForm.ResetForm();
            m_SpecificationForm.ResetForm();
            m_OwnerDataForm.ResetForm();
            m_EnumForm.ResetForm();
        }

        public void AddNewVehicle()
        {
            float remainingBatteryOrFuelTimeInHours = 0;
            string modelName;
            Console.WriteLine("--Add a new vehicle picked--");
            VehicleFactory.VehicleTypeStruct vehiclePicked = m_VehicleTypeForm.DisplayAndGetResult(m_Garage.VehicleTypesList);
            string licensePlate = m_LicensePlateForm.DisplayAndGetResult();

            try
            {
                Vehicle foundVehicle = m_Garage.getVehicleByLicensePlateNumber(licensePlate);
                Console.WriteLine(String.Format("A {0} was found with this license plate number: {1}{2}Changed this vehicle status to repair in progress"), 
                    foundVehicle.Specifications.VehicleType, foundVehicle.ModelName, Environment.NewLine);
                m_Garage.SetVehicleRepairStatus(licensePlate, Vehicle.eRepairStatus.IN_PROGRESS);
            }
            catch (Exception ex)
            {
                List<Vehicle.Wheel> wheels = m_WheelsForm.DisplayAndGetResult(vehiclePicked);
                Console.WriteLine("Enter model name:");
                modelName = Console.ReadLine();
                remainingBatteryOrFuelTimeInHours = m_RemainingEnergyOrFuelForm.DisplayAndGetResult(vehiclePicked);
                Specifications specifications = m_SpecificationForm.DisplayAndGetResult(vehiclePicked);
                Vehicle.VehicleOwnerData ownerData = m_OwnerDataForm.DisplayAndGetResult();
                // Call to factory add new vehicle
                Vehicle vehicleToBeAdded = m_VehicleFactory.createVehicle(vehiclePicked, modelName, licensePlate, wheels, ownerData, specifications, remainingBatteryOrFuelTimeInHours);
                m_Garage.AddVehicle(vehicleToBeAdded);
            }
        }
        public void DisplayLicensePlates()
        {
            Console.WriteLine("-- Display license plates option picked --");
            Console.WriteLine("List of all license plates of vehicles currently in the garage:");
            foreach (string licensePlateNumber in m_Garage.GetLicensePlateNumbers())
            {
                Console.WriteLine(licensePlateNumber);
            }
        }
        public void ChangeRepairStatusOfSpecificVehicle()
        {
            string licensePlateNumber;
            Vehicle.eRepairStatus repairStatus;

            Console.WriteLine("-- Change repair status of specific vehicle option picked --");
            licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();

            repairStatus = (Vehicle.eRepairStatus)m_EnumForm.DisplayAndGetResult("Please enter repair status to update to:", typeof(Vehicle.eRepairStatus).GetEnumValues());
            try
            {
                m_Garage.SetVehicleRepairStatus(licensePlateNumber, repairStatus);
                Console.WriteLine(String.Format("Successfully changed repair status to {0}", repairStatus.ToString()));
            }
            catch (NoSuchVehicleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FuelVehicle()
        {
            string licensePlateNumber;
            GasolineFueledVehicle.eFuelType fuelType;
            float fuelAmountInLiters;

            Console.WriteLine("-- Fuel vehicle option picked --");
            licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();
            fuelType = (GasolineFueledVehicle.eFuelType)m_EnumForm.DisplayAndGetResult("Please enter fuel type:", typeof(GasolineFueledVehicle.eFuelType).GetEnumValues());
            Console.WriteLine("Please enter amount of fuel in liters:");
            if (float.TryParse(Console.ReadLine(), out fuelAmountInLiters))
            {
                try
                {
                    m_Garage.FuelVehicle(licensePlateNumber, fuelType, fuelAmountInLiters);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        public void InflateVehicleTirexToMax()
        {

        }
        public void ChargeElectricVehicle()
        {

        }
        public void DisplayAllInfoAboutSpecificVehicle()
        {

        }
    }
}
