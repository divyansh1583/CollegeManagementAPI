	use test;

	CREATE TABLE DC_UserDetail (
		UserId INT IDENTITY(1,1) PRIMARY KEY,
		FirstName NVARCHAR(20) NOT NULL,
		LastName NVARCHAR(50) NOT NULL,
		Email NVARCHAR(255) NOT NULL,
		PhoneNumber NVARCHAR(15) NOT NULL,
		CountryId INT NOT NULL,
		StateId INT NOT NULL,
		Gender NVARCHAR(10) NOT NULL,
		IsDeleted BIT NOT NULL DEFAULT 0,
		FOREIGN KEY (CountryId) REFERENCES DC_Country(CountryId),
		FOREIGN KEY (StateId) REFERENCES DC_State(StateId)
	);

	CREATE TABLE DC_LoginCredentials (
		UserId INT NOT NULL,
		Email NVARCHAR(255) NOT NULL,
		Password NVARCHAR(255) NOT NULL,
		IsActive BIT NOT NULL DEFAULT 1,
		FOREIGN KEY (UserId) REFERENCES DC_UserDetail(UserId)
	);


	CREATE PROCEDURE DC_InsertUserAndLoginCredentials
		@FirstName NVARCHAR(20),
		@LastName NVARCHAR(50),
		@Email NVARCHAR(255),
		@PhoneNumber NVARCHAR(15),
		@CountryId INT,
		@StateId INT,
		@Gender NVARCHAR(10),
		@Password NVARCHAR(255)
	AS
	BEGIN

			-- Insert into UserDetail
			IF NOT EXISTS (SELECT 1 FROM DC_LoginCredentials WHERE Email = @Email)
		BEGIN 
			INSERT INTO DC_UserDetail (FirstName, LastName, Email, PhoneNumber, CountryId, StateId, Gender,  IsDeleted)
			VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @CountryId, @StateId, @Gender, 0);

			-- Get the newly generated UserId
			DECLARE @NewUserId INT;
			SET @NewUserId = SCOPE_IDENTITY();

			-- Insert into LoginCredentials
			INSERT INTO DC_LoginCredentials (UserId, Email, Password, IsActive)
			VALUES (@NewUserId, @Email, @Password, 1);

		END 

			ELSE
		BEGIN
			-- If the email already exists, raise an error
			RAISERROR ('Email already exists in the database', 16, 1);
			RETURN;
		END
	END;



	DECLARE @FirstName NVARCHAR(20) = 'Bob';
	DECLARE @LastName NVARCHAR(50) = 'Johnson';
	DECLARE @Email NVARCHAR(255) = 'bob.johnson@example.com';
	DECLARE @PhoneNumber NVARCHAR(15) = '5551234567';
	DECLARE @CountryId INT = 3;  -- Assume 3 corresponds to a valid country
	DECLARE @StateId INT = 3;    -- Assume 3 corresponds to a valid state
	DECLARE @Gender NVARCHAR(10) = 'Male';
	DECLARE @Password NVARCHAR(255) = 'Password789!';

	DECLARE @FirstName NVARCHAR(20) = 'Jane';
	DECLARE @LastName NVARCHAR(50) = 'Smith';
	DECLARE @Email NVARCHAR(255) = 'jane.smith@example.com';
	DECLARE @PhoneNumber NVARCHAR(15) = '0987654321';
	DECLARE @CountryId INT = 2;  -- Assume 2 corresponds to a valid country
	DECLARE @StateId INT = 2;    -- Assume 2 corresponds to a valid state
	DECLARE @Gender NVARCHAR(10) = 'Female';
	DECLARE @Password NVARCHAR(255) = 'Password456!';

	EXEC DC_InsertUserAndLoginCredentials
		@FirstName,
		@LastName,
		@Email,
		@PhoneNumber,
		@CountryId,
		@StateId,
		@Gender,
		@Password;

	

		SELECT * FROM DC_UserDetail
		SELECT * FROM DC_LoginCredentials

		SELECT * FROM DC_UserDetail WHERE Email = 'john.doe@example.com';
		SELECT * FROM DC_LoginCredentials WHERE Email = 'john.doe@example.com';

		SELECT * FROM DC_AdminUsers
		
