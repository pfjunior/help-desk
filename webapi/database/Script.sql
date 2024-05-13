USE [HelpDesk]

-- Departments
INSERT INTO Departments (Id, Code, Name) VALUES (NEWID(), 'IT', 'Information Technology');
INSERT INTO Departments (Id, Code, Name) VALUES (NEWID(), 'FIN', 'Finance');

-- AspNetUsers
DECLARE @adminUserId uniqueidentifier
SET @adminUserId = NEWID()

DECLARE @supportUserId uniqueidentifier
SET @supportUserId = NEWID()

DECLARE @userUserId uniqueidentifier
SET @userUserId = NEWID()

INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES(@adminUserId, 'admin@email.com', UPPER('admin@email.com'), 'admin@email.com', UPPER('admin@email.com'), 1, 'AQAAAAIAAYagAAAAEJKub3XGAtwA+f9uQ/jxiqoJNZTmiUbmyUx1N0YUktuMSgghV9Y1bQPBeNZIzetDTA==', 'SY6MFDHTYGBUWHB6YMZWK33ANPSD6WK2', '9576f143-2e29-4f1c-a81e-2a5ce503b999', null, 0, 0, null, 1, 0);

INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES(@supportUserId, 'support@email.com', UPPER('support@email.com'), 'support@email.com', UPPER('support@email.com'), 1, 'AQAAAAIAAYagAAAAEOEdXUoopoGatFzw1QkK3NKxXOcoI2p9ACFQxsxY9ZlUfS93qx5jBjgFfwAVO68/hg==', 'FUKITGWX4EE65M4PFMABKPGB5A3HIB6B', 'aa98d511-7806-451e-9573-040271422215', null, 0, 0, null, 1, 0);

INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES(@userUserId, 'user@email.com', UPPER('user@email.com'), 'user@email.com', UPPER('user@email.com'), 1, 'AQAAAAIAAYagAAAAEJu0X/Scm2RjikSKNBt3RJCCkf8kKCgRdSWZfE+O6DCOlWFmm8fHba7Oqukvr04NqA==', 'XSDJECGY6OAJKVUZI52EX7F6GM5B2XL7', '28f265a4-b5f8-4612-883f-a5dbdf25f60c', null, 0, 0, null, 1, 0);

-- Users
INSERT INTO Users (Id, FirstName, LastName, Email, PhoneNumber, Extension, Active, CreatedAt, UpdatedAt, Department_Code, Department_Name)
VALUES(@adminUserId, 'Jarvis', 'Edwin', 'admin@email.com', null, null, 1, GETDATE(), null, 'IT', 'Information Technology');

INSERT INTO Users (Id, FirstName, LastName, Email, PhoneNumber, Extension, Active, CreatedAt, UpdatedAt, Department_Code, Department_Name)
VALUES(@supportUserId, 'Peter', 'Park', 'support@email.com', null, null, 1, GETDATE(), null, 'IT', 'Information Technology');

INSERT INTO Users (Id, FirstName, LastName, Email, PhoneNumber, Extension, Active, CreatedAt, UpdatedAt, Department_Code, Department_Name)
VALUES(@userUserId, 'Mary', 'Jane', 'user@email.com', null, null, 1, GETDATE(), null, 'FIN', 'Finance');

-- AspNetUserClaims
INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue) VALUES(@adminUserId, 'Administrator', 'Admin');
INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue) VALUES(@supportUserId, 'Support', 'Support');