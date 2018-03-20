

USE [master]
GO


DROP Database IF EXISTS BespokeCloud_CustomerTest
Go

Create Database BespokeCloud_CustomerTest
Go

USE [BespokeCloud_CustomerTest]
GO

-- Customers Table -----------------------------------
-- Customers Table -----------------------------------
-- Customers Table -----------------------------------

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](  
    [Id][int] IDENTITY(1, 1) NOT NULL, 
	[FirstName][varchar](50) NOT NULL, 
	[LastName][varchar](50) NOT NULL, 
	[Email][varchar](100) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- DELETE FROM Customers
-- Customers Inserts -----------------------------------
-- Customers Inserts -----------------------------------
-- Customers Inserts -----------------------------------
INSERT INTO [dbo].[Customers] (FirstName, LastName, Email) VALUES ('Bryan', 'Smith', 'bryan.smith@gmail.com');
INSERT INTO [dbo].[Customers] (FirstName, LastName, Email) VALUES ('Paul', 'Johns', 'paul.johns@live.com.au');
INSERT INTO [dbo].[Customers] (FirstName, LastName, Email) VALUES ('Errol', 'Willenberg', 'errol@bubbanoosh.com.au');
INSERT INTO [dbo].[Customers] (FirstName, LastName, Email) VALUES ('Kate', 'Illsley', 'kate.illsley@methodrecruitment.com.au');
GO


-- Procs - Customers_GetAllCustomers ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Customers_GetAllCustomers]
Go

CREATE PROCEDURE [dbo].[Customers_GetAllCustomers]  
(
	@SearchText VARCHAR (50) = ''
)  
AS 
/*
-- Test harness
EXEC Customers_GetAllCustomers 'n@'

*/ 
	SET NOCOUNT ON --Boost Network performance

	DECLARE @SEARCH VARCHAR(50) = LTRIM(RTRIM(ISNULL(@SearchText, '')))

	If ISNULL(@SEARCH, '') = ''
	BEGIN 

		SELECT * FROM Customers;  

	END ELSE BEGIN

		DECLARE @EMAILPOS INT = CHARINDEX('@', @SEARCH)

		--By Name
		IF @EMAILPOS = 0
		BEGIN

			SELECT Id, FirstName, LastName, Email FROM Customers c WHERE c.FirstName LIKE '%' + @SEARCH + '%' OR  c.LastName LIKE '%' + @SEARCH + '%';

		--By Email
		END ELSE BEGIN

			SELECT Id, FirstName, LastName, Email FROM Customers c WHERE c.Email LIKE '%' + @SEARCH + '%';

		END
	END
GO 

-- Procs - Customers_GetCustomerById ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Customers_GetCustomerById]
Go

CREATE PROCEDURE [dbo].[Customers_GetCustomerById]  
(
	@Id INT
)  
AS 
/*
-- Test harness
EXEC Customers_GetCustomerById 1

*/ 
	SET NOCOUNT ON --Boost Network performance

	SELECT Id, FirstName, LastName, Email FROM Customers c WHERE c.Id = @Id;

GO 

-- Procs - Customers_AddCustomer ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Customers_AddCustomer]
Go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Customers_AddCustomer
  (
		@Id int OUTPUT,
		@FirstName VARCHAR(50),
		@LastName VARCHAR(50),
		@Email VARCHAR(100)
  )
AS
/*
-- Test Harness
sp_help Customers_AddCustomer

EXEC Customers_AddCustomer
		@Id = NULL,
		@FirstName = 'Feral',
		@LastName = 'Smith',
		@Email = 'ferfffal@stufff.com.au'

SELECT * FROM Customers;
*/
 --Network Performance. Change to SET NOCOUNT ON to stop SQL returning (1 row(s) affected)
 --Change to SET NOCOUNT OFF to 'return the affected rows during UPDATE & DELETE'
 --During INSERT Return '@id = SCOPE_IDENTITY()'
 SET NOCOUNT ON
 
	IF NOT EXISTS (SELECT 1 FROM Customers WHERE Email = @Email)
	BEGIN
            INSERT INTO Customers (
				FirstName, LastName, Email
			) VALUES (
				@FirstName, @LastName, @Email
            );
	END

    SET @Id = SCOPE_IDENTITY()
    IF ISNULL(@Id,0) = 0
    BEGIN
		SET @Id = 0
	END ELSE BEGIN
		SET @Id = CAST(SCOPE_IDENTITY() AS INT)
	END
	

GO

-- Procs - Customers_UpdateCustomer ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Customers_UpdateCustomer]
Go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Customers_UpdateCustomer
  (
		@Id int,
		@FirstName VARCHAR(50),
		@LastName VARCHAR(50),
		@Email VARCHAR(100)
  )
AS
/*
-- Test Harness
sp_help Customers_UpdateCustomer

EXEC Customers_UpdateCustomer
		@Id = 1,
		@FirstName = 'Feral',
		@LastName = 'Smith',
		@Email = 'e@live.com.au'


SELECT * FROM Customers;

*/

 --Network Performance. Change to SET NOCOUNT ON to stop SQL returning affected Rows too
 --During INSERT Return '@id = SCOPE_IDENTITY()'
 SET NOCOUNT OFF --To 'return the affected rows during UPDATE & DELETE'
 
    UPDATE Customers 
	SET 
		FirstName = @FirstName,
		LastName = @LastName,
		Email = @Email
		WHERE Id = @Id;

GO

-- Procs - Customers_DeleteCustomer ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Customers_DeleteCustomer]
Go

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Customers_DeleteCustomer
(
	@Id INT
)
AS
/*
--Test harness
--SELECT * FROM Customers

EXEC Customers_DeleteCustomer 4 
*/
SET NOCOUNT ON

BEGIN TRANSACTION


	SET NOCOUNT OFF --Rows affected here
	DELETE FROM		Customers
	WHERE			Id = @Id
	
	IF @@ERROR != 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
COMMIT TRANSACTION

GO




-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------
-- Users Table --------------------------------------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](  
    [Id][int] IDENTITY(1, 1) NOT NULL, 
	[DateCreated][smalldatetime] NOT NULL, 
	[FirstName][varchar](50) NOT NULL, 
	[LastName][varchar](50) NOT NULL, 
	[Email][varchar](100) NOT NULL,
	[PasswordHash] VARBINARY(64)  NOT NULL,
	[PasswordSalt] VARBINARY(128) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- DELETE FROM Users

-- Procs - Users_Register ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_Register]
Go

		SET ANSI_NULLS ON
		GO
		SET QUOTED_IDENTIFIER ON
		GO

		CREATE PROCEDURE Users_Register
		  (
				@Id int OUTPUT,
				@FirstName VARCHAR(50),
				@LastName VARCHAR(50),
				@Email VARCHAR(100),
				@PasswordHash VARBINARY(64),
				@PasswordSalt VARBINARY(128)
		  )
		AS
		/*
		-- Test Harness
		sp_help Users_Register

		EXEC Users_Register
				@Id = NULL,
				@FirstName = 'Feral',
				@LastName = 'Smith',
				@Email = 'e@live.com.au'

		SELECT * FROM Users;
		*/
		 --Network Performance. Change to SET NOCOUNT ON to stop SQL returning (1 row(s) affected)
		 --Change to SET NOCOUNT OFF to 'return the affected rows during UPDATE & DELETE'
		 --During INSERT Return '@id = SCOPE_IDENTITY()'
		 SET NOCOUNT ON
 
			IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
			BEGIN
					INSERT INTO Users (
						DateCreated, FirstName, LastName, Email, PasswordHash, PasswordSalt
					) VALUES (
						GETDATE(), @FirstName, @LastName, @Email, @PasswordHash, @PasswordSalt
					);
			END

			SET @Id = SCOPE_IDENTITY()
			IF ISNULL(@Id,0) = 0
			BEGIN
				SET @Id = 0
			END ELSE BEGIN
				SET @Id = CAST(SCOPE_IDENTITY() AS INT)
			END
	

		GO

		-- Procs - Users_GetAllUsers ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_GetAllUsers]
Go

		CREATE PROCEDURE [dbo].[Users_GetAllUsers]  
		(
			@SearchText VARCHAR (100) = ''
		)  
		AS 
		/*
		-- Test harness
		EXEC Users_GetAllUsers 'n@'

		*/ 
			SET NOCOUNT ON --Boost Network performance

			DECLARE @SEARCH VARCHAR(100) = LTRIM(RTRIM(ISNULL(@SearchText, '')))

			If ISNULL(@SEARCH, '') = ''
			BEGIN 

				SELECT * FROM Users;  

			END ELSE BEGIN

				DECLARE @EMAILPOS INT = CHARINDEX('@', @SEARCH)

				--By Name
				IF @EMAILPOS = 0
				BEGIN

					SELECT Id, FirstName, LastName, Email FROM Users u WHERE u.FirstName LIKE '%' + @SEARCH + '%' OR  u.LastName LIKE '%' + @SEARCH + '%';

				--By Email
				END ELSE BEGIN

					SELECT Id, FirstName, LastName, Email FROM Users u WHERE u.Email LIKE '%' + @SEARCH + '%';

				END
			END
		GO 

-- Procs - Users_GetUserByUsername ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_GetUserByUsername]
Go

		CREATE PROCEDURE [dbo].[Users_GetUserByUsername]  
		(
			@Email VARCHAR (100)
		)  
		AS 
		/*
		-- Test harness
		EXEC Users_GetUserByUsername 'e@bubbanoosh.com.au'

		*/ 
			SET NOCOUNT ON --Boost Network performance

			SELECT Id, FirstName, LastName, Email, PasswordHash, PasswordSalt FROM Users u WHERE u.Email = LTRIM(RTRIM(@Email));

		GO 



-- Procs - Users_GetUserById ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_GetUserById]
Go

		CREATE PROCEDURE [dbo].[Users_GetUserById]  
		(
			@Id INT
		)  
		AS 
		/*
		-- Test harness
		EXEC Users_GetUserById 1

		*/ 
			SET NOCOUNT ON --Boost Network performance

			SELECT Id, FirstName, LastName, Email FROM Users u WHERE u.Id = @Id;

		GO 



-- Procs - Users_UpdateUser ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_UpdateUser]
Go

		SET ANSI_NULLS ON
		GO
		SET QUOTED_IDENTIFIER ON
		GO

		CREATE PROCEDURE Users_UpdateUser
		  (
				@Id int,
				@FirstName VARCHAR(50),
				@LastName VARCHAR(50),
				@Email VARCHAR(100)
		  )
		AS
		/*
		-- Test Harness
		sp_help Users_UpdateUser

		EXEC Users_UpdateUser
				@Id = 1,
				@FirstName = 'Feral',
				@LastName = 'Smith',
				@Email = 'e@live.com.au'


		SELECT * FROM Users;

		*/

		 --Network Performance. Change to SET NOCOUNT ON to stop SQL returning affected Rows too
		 --During INSERT Return '@id = SCOPE_IDENTITY()'
		 SET NOCOUNT OFF --To 'return the affected rows during UPDATE & DELETE'
 
			UPDATE Users 
			SET 
				FirstName = @FirstName,
				LastName = @LastName,
				Email = @Email
				WHERE Id = @Id;

		GO

-- Procs - Users_UpdatePassword ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_UpdatePassword]
Go

		SET ANSI_NULLS ON
		GO
		SET QUOTED_IDENTIFIER ON
		GO

		CREATE PROCEDURE Users_UpdatePassword
		  (
				@Id int,
				@PasswordHash VARBINARY(64),
				@PasswordSalt VARBINARY(128)
		  )
		AS
		/*
		-- Test Harness
		sp_help Users_UpdatePassword

		EXEC Users_UpdatePassword
				@Id = 1,
				@PasswordHash = 11111,
				@PasswordSalt = 2222


		SELECT * FROM Users;

		*/

		 --Network Performance. Change to SET NOCOUNT ON to stop SQL returning affected Rows too
		 --During INSERT Return '@id = SCOPE_IDENTITY()'
		 SET NOCOUNT OFF --To 'return the affected rows during UPDATE & DELETE'
 
			UPDATE Users 
			SET 
				PasswordHash = @PasswordHash,
				PasswordSalt = @PasswordSalt
				WHERE Id = @Id;

		GO

		-- Procs - Users_DeleteUser ----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------
-- Procs -----------------------------------------------

DROP PROCEDURE IF EXISTS [dbo].[Users_DeleteUser]
Go

		SET ANSI_NULLS ON
		GO

		SET QUOTED_IDENTIFIER ON
		GO

		CREATE PROCEDURE Users_DeleteUser
		(
			@Id INT
		)
		AS
		/*
		--Test harness
		--SELECT * FROM Users

		EXEC Users_DeleteUser 4 
		*/
		SET NOCOUNT ON

		BEGIN TRANSACTION


			SET NOCOUNT OFF --Rows affected here
			DELETE FROM		Users
			WHERE			Id = @Id
	
			IF @@ERROR != 0
			BEGIN
				ROLLBACK TRANSACTION
				RETURN
			END
		COMMIT TRANSACTION

		GO
