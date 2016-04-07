﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.Models;

namespace CarFuel.DataAccess
{
    public class FakeCarDb : ICarDb
    {
        private ICollection<Car> cars;

        public bool AddMethodHasCalled { get; set; } = false;

        public FakeCarDb()
        {
            cars = new HashSet<Car>();
        }

        public Car Add(Car item)
        {
            cars.Add(item);

            AddMethodHasCalled = true;
            return item;
        }

        public Car Get(Guid id)
        {
            return cars.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Car> GetAll(Func<Car, bool> predicate)
        {
            return cars.Where(predicate).AsEnumerable();
        }
    }
}
