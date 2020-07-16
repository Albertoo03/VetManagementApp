using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VetManagementApp.Model;

namespace VetManagementAppUnitTests
{
    [TestClass]
    public class VetManagementAppUnitTest
    {
        [TestMethod]
        public void AddNewAnimalSpeciesAndRemoveIt()
        {
            using (var uow = new UnitOfWork())
            {
                string nameOfAnimalToAdd = "Cow";
                var numberOfAnimalSpeciesBeforeAnyAction = uow.AnimalBasicInfos.All.Count;

                uow.Save();
                AnimalBasicInfo animalBasicInfo = new AnimalBasicInfo();
                animalBasicInfo.Species = nameOfAnimalToAdd;
                animalBasicInfo.Group = VetManagementApp.Helpers.HelpfulUtilities.AnimalGroup.Mammal;

                uow.AnimalBasicInfos.Add(animalBasicInfo);

                uow.Save();

                var numberOfAnimalSpeciesAfterAdd = uow.AnimalBasicInfos.All.Count;

                Assert.AreEqual(numberOfAnimalSpeciesAfterAdd, numberOfAnimalSpeciesBeforeAnyAction + 1);

                uow.AnimalBasicInfos.Delete(animal => animal.Species == nameOfAnimalToAdd);

                uow.Save();

                var numberOfAnimalSpeciesAfterRemove = uow.AnimalBasicInfos.All.Count;

                Assert.AreEqual(numberOfAnimalSpeciesBeforeAnyAction, numberOfAnimalSpeciesAfterRemove);
            }
        }

        [TestMethod]
        public void CheckIfNumberOfOwnedAnimalsIsRight()
        {
            using (var uow = new UnitOfWork())
            {
                var listOfCustomers = uow.Customers.All;
                var sampleClient = listOfCustomers.FirstOrDefault();

                if (sampleClient == null)
                    return;

                var numberOfOwnedAnimals = sampleClient.OwnedAnimals.Count();

                var listOfAssignedAnimalsFromAnimalsTable = uow.Animals.GetMultipleByPredicate(animal => animal.Owner.Id == sampleClient.Id).ToList();

                var numberOfAnimalsAssignedToSampleClient = listOfAssignedAnimalsFromAnimalsTable.Count();

                Assert.AreEqual(numberOfOwnedAnimals, numberOfAnimalsAssignedToSampleClient);
            }
        }
    }
}
