CREATE TABLE users (
      name VARCHAR(50) PRIMARY KEY,
      password VARCHAR(100)
);
CREATE TABLE accounts (
        id char(38) NOT NULL  PRIMARY KEY,
        platform VARCHAR(25),
        login VARCHAR(50),
        password VARCHAR(100),
        username VARCHAR(50)
);