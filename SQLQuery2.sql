use test;
CREATE TABLE DC_Country (
    CountryId INT IDENTITY(1,1) PRIMARY KEY,
    CountryName NVARCHAR(50) NOT NULL
);

CREATE TABLE DC_State (
    StateId INT IDENTITY(1,1) PRIMARY KEY,
    CountryId INT NOT NULL,
    StateName NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES DC_Country(CountryId)
);

INSERT INTO DC_Country (CountryName) VALUES ('USA');
INSERT INTO DC_Country (CountryName) VALUES ('Canada');
INSERT INTO DC_Country (CountryName) VALUES ('UK');
INSERT INTO DC_Country (CountryName) VALUES ('Australia');
INSERT INTO DC_Country (CountryName) VALUES ('India');


-- States for USA (CountryId = 1)
INSERT INTO DC_State (CountryId, StateName) VALUES (1, 'California');
INSERT INTO DC_State (CountryId, StateName) VALUES (1, 'Florida');
INSERT INTO DC_State (CountryId, StateName) VALUES (1, 'New York');
INSERT INTO DC_State (CountryId, StateName) VALUES (1, 'Texas');

-- States for Canada (CountryId = 2)
INSERT INTO DC_State (CountryId, StateName) VALUES (2, 'Alberta');
INSERT INTO DC_State (CountryId, StateName) VALUES (2, 'British Columbia');
INSERT INTO DC_State (CountryId, StateName) VALUES (2, 'Ontario');
INSERT INTO DC_State (CountryId, StateName) VALUES (2, 'Quebec');

-- States for UK (CountryId = 3)
INSERT INTO DC_State (CountryId, StateName) VALUES (3, 'England');
INSERT INTO DC_State (CountryId, StateName) VALUES (3, 'Scotland');
INSERT INTO DC_State (CountryId, StateName) VALUES (3, 'Wales');
INSERT INTO DC_State (CountryId, StateName) VALUES (3, 'Northern Ireland');

-- States for Australia (CountryId = 4)
INSERT INTO DC_State (CountryId, StateName) VALUES (4, 'New South Wales');
INSERT INTO DC_State (CountryId, StateName) VALUES (4, 'Queensland');
INSERT INTO DC_State (CountryId, StateName) VALUES (4, 'Victoria');
INSERT INTO DC_State (CountryId, StateName) VALUES (4, 'Western Australia');

-- States for India (CountryId = 5)
INSERT INTO DC_State (CountryId, StateName) VALUES (5, 'Delhi');
INSERT INTO DC_State (CountryId, StateName) VALUES (5, 'Maharashtra');
INSERT INTO DC_State (CountryId, StateName) VALUES (5, 'Karnataka');
INSERT INTO DC_State (CountryId, StateName) VALUES (5, 'Tamil Nadu');
