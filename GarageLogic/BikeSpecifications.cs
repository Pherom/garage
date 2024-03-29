﻿using System;
using System.Collections.Generic;

namespace GarageLogic
{
    internal class BikeSpecifications : Specifications
    {
        public enum eLicenseType
        {
            A,
            A1,
            BB,
            B1
        }

        private eLicenseType m_LicenseType;
        private int m_EngineCapacityInCC;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineCapacityInCC
        {
            get
            {
                return m_EngineCapacityInCC;
            }
        }

        public override void InitSpecifications(List<object> i_SpecificationAnswers)
        {
            if ((int)i_SpecificationAnswers[1] <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            m_LicenseType = (eLicenseType)i_SpecificationAnswers[0];
            m_EngineCapacityInCC = (int)i_SpecificationAnswers[1];
        }

        public BikeSpecifications(string i_TypeOfVehicle) : base(i_TypeOfVehicle) { }
    }
}
