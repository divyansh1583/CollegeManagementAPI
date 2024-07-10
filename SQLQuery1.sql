	use test;

	CREATE TABLE DC_UserDetail (
		UserId INT IDENTITY(1,1) PRIMARY KEY,
		FirstName NVARCHAR(20) NOT NULL,
		LastName NVARCHAR(50) NOT NULL,
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
	SELECT * FROM DC_LoginCredentials lc WHERE Email='jane.smith@example.com'

	--insert procedure
	CREATE or ALTER PROCEDURE DC_InsertUserAndLoginCredentials
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

			
			IF NOT EXISTS (SELECT 1 FROM DC_LoginCredentials WHERE Email = @Email)
		BEGIN 
			INSERT INTO DC_UserDetail (FirstName, LastName, PhoneNumber, CountryId, StateId, Gender,  IsDeleted)
			VALUES (@FirstName, @LastName, @PhoneNumber, @CountryId, @StateId, @Gender, 0);

			-- Get the newly generated UserId
			DECLARE @NewUserId INT;
			SET @NewUserId = SCOPE_IDENTITY();

			INSERT INTO DC_LoginCredentials (UserId, Email, Password, IsActive)
			VALUES (@NewUserId, @Email, @Password, 1);

		END 

			ELSE
		BEGIN
			RAISERROR ('Email already exists in the database', 16, 1);
			RETURN;
		END
	END;

	--update procrdure
	CREATE PROCEDURE DC_UpdateUserAndLoginCredentials 
		@UserId INT,
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
		UPDATE DC_UserDetail
		SET FirstName = @FirstName,
			LastName = @LastName,
			PhoneNumber = @PhoneNumber,
			CountryId = @CountryId,
			StateId = @StateId,
			Gender = @Gender
		WHERE UserId = @UserId;
	
		UPDATE DC_LoginCredentials
		SET Email = @Email,
			Password = @Password
		WHERE UserId = @UserId;
	END;
	
	--delelte procedure
	CREATE PROCEDURE DC_DeleteUserAndLoginCredentials @UserId INT
	AS
	BEGIN
		DELETE
		FROM DC_LoginCredentials
		WHERE UserId = @UserId;
	
		
		DELETE
		FROM DC_UserDetail
		WHERE UserId = @UserId;
	END;
	
	--GET LIST
	CREATE OR ALTER PROCEDURE DC_GetUserDetails
	AS
	BEGIN
		SELECT ud.UserId,
			ud.FirstName,
			ud.LastName,
			ud.PhoneNumber,
			c.CountryName,
			s.StateName,
			ud.Gender,
			lc.Email
		FROM DC_UserDetail ud
		INNER JOIN DC_LoginCredentials lc
			ON ud.UserId = lc.UserId
		INNER JOIN DC_Country c
			ON ud.CountryId = c.CountryId
		INNER JOIN DC_State s
			ON ud.StateId = s.StateId
	END;
	
		EXEC DC_InsertUserAndLoginCredentials 'Jane', 'Smith',	'jane.smith@example.com','0987654321',2,2,'Female',	'Password456!';
		EXEC DC_UpdateUserAndLoginCredentials 1, 'Jane', 'Smith', 'jane.smith@example.com', '0987654321', 2, 2, 'Female', 'NewPassword123!';
		EXEC DC_DeleteUserAndLoginCredentials 1;
		EXEC DC_GetUserDetails;


		use test;
		SELECT * FROM DC_UserDetail
		SELECT * FROM DC_LoginCredentials
	
		SELECT * FROM DC_UserDetail WHERE Email = 'john.doe@example.com';
		SELECT * FROM DC_LoginCredentials WHERE Email = 'john.doe@example.com';
	
		SELECT * FROM DC_AdminUsers
	
		truncate table DC_AdminUsers
			
	