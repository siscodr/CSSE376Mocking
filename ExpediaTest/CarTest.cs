using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestGetCarLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();

            Expect.Call(mockDB.getCarLocation(22)).Return("China Town");
            Expect.Call(mockDB.getCarLocation(103)).Return("Terre Haute");

            mocks.ReplayAll();

            Car target = new Car(10);

            target.Database = mockDB;

            String result;

            result = target.getCarLocation(22);
            Assert.AreEqual("China Town", result);

            result = target.getCarLocation(103);
            Assert.AreEqual("Terre Haute", result);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestMileage()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = 5000;

            var target = ObjectMother.BMW();
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(5000, mileage);
        }
	}
}
