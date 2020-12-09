DROP TABLE Users CASCADE;
CREATE TABLE Users (
	user_id integer PRIMARY KEY,
	username varchar(50) NOT NULL,
	passwrd varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	phonenumber char(10),
	flagged bool,
	city varchar(50) NOT NULL
);
DROP TABLE Product CASCADE;
CREATE TABLE Product (
	product_id integer PRIMARY KEY,
	userID integer,
	dutchName varchar(50) NOT NULL,
	latinName varchar(100),
	description varchar(500) NOT NULL,
	picture varchar(100) NOT NULL,
	plantKind varchar(10) NOT NULL,
	plantType varchar(10) NOT NULL,
	lightAmount varchar(4) NOT NULL,
	waterAmount varchar(4) NOT NULL,
	trade boolean NOT NULL,
	FOREIGN KEY (userID) REFERENCES Users(user_id)
);

INSERT INTO Users
VALUES (1, 'Jan Jansen', 'wachtwoord', 'jjansen@gmail.com', null, false, 'Rotterdam'),
		(3, 'Piet Smidt', 'w8woord', 'pietsmidt@yahoo.com', '0612937489', false, 'Rotterdam'),
		(6, 'epic_gamer_99', 'gameing', 'gamer@games.com', null, true, 'Barendrecht'),
		(10, 'josef0320', 'nietmeteenph', 'josef0320@outlook.com', null, false, 'Gouda'),
		(15, 'Meadow', 'password', 'meadow@ziggo.com', '0107532045', false, 'Rotterdam');

INSERT INTO Product
VALUES	(21, 1, 'Hortensia', 'Hydrangea macrophylla', 'een hortensia', 'picture', 'seed', 'herb', 'high', 'mid', false),
		(28, 3, 'Eik', 'Quercus', 'grote boom', 'picture', 'cutting', 'tree', 'mid', 'low', false),
		(36, 6, 'gamerplant', null, 'geen kamerplant', 'picture', 'bud', 'creeper', 'low', 'low', true),
		(45, 10, 'Vijgenstruik', null, 'kan vruchten opleveren na paar jaar groei', 'picture', 'cutting', 'shrub', 'mid', 'high', true),
		(55, 15, 'Wolfsklauw', 'Lycopodium clavatum','op koude plek laten groeien!', 'picture', 'bud', 'climber', 'low', 'high', false);
		
SELECT *
FROM Product;