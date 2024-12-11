namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCheckLoginWithValidCredentials()
        {
            // Arrange
            var email = "john@example.com";
            var password = "password123!";
            var accountsLogic = new AccountsLogic();

            var expectedUser = new UserModel(
                name: "John Doe",
                emailAddress: email,
                phoneNumber: "1234567890",
                password: password,
                dateOfBirth: "1990-01-01",
                address: "123 Main St",
                preferences: new List<string>()
            );

            accountsLogic.AddAccountForTesting(expectedUser); // Add test data

            // Act
            var result = accountsLogic.CheckLogin(email, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.EmailAddress, result.EmailAddress);
            Assert.AreEqual(expectedUser.Name, result.Name);
        }

        [TestMethod]
        public void TestCheckLoginWithInvalidCredentials()
        {
            // Arrange
            var email = "john@example.com"; // Attempting to log in with this email
            var password = "wrongpassword"; // Incorrect password
            var accountsLogic = new AccountsLogic();

            var testUser = new UserModel(
                name: "John Doe",
                emailAddress: email,
                phoneNumber: "1234567890",
                password: "password123!", // Correct password for the user
                dateOfBirth: "1990-01-01",
                address: "123 Main St",
                preferences: new List<string>()
            );

            accountsLogic.AddAccountForTesting(testUser); // Add test data

            // Act
            var result = accountsLogic.CheckLogin(email, password);

            // Assert
            Assert.IsNull(result); // User should not be authenticated
        }

        [TestMethod]
        public void TestCheckLoginWithNonexistentEmail()
        {
            // Arrange
            var email = "nonexistent@example.com"; // Email not present in the system
            var password = "password123!";
            var accountsLogic = new AccountsLogic();

            var testUser = new UserModel(
                name: "John Doe",
                emailAddress: "john@example.com", // Different email in the system
                phoneNumber: "1234567890",
                password: password,
                dateOfBirth: "1990-01-01",
                address: "123 Main St",
                preferences: new List<string>()
            );

            accountsLogic.AddAccountForTesting(testUser); // Add test data

            // Act
            var result = accountsLogic.CheckLogin(email, password);

            // Assert
            Assert.IsNull(result); // No user should be authenticated
        }
    }
}
