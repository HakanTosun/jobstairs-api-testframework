using Bogus;
using JobStairsSpecflowTest.Models;

namespace JobStairsSpecflowTest.Helpers
{
    public static class TestdatenGenerator
    {
        public static Bewerber ErzeugeBewerber()
        {
            var faker = new Faker("de");
            return new Bewerber
            {
                Name = faker.Name.FullName(),
                Email = faker.Internet.Email(),
                Position = "Testentwickler"
            };
        }
    }
}