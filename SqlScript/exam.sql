CREATE TABLE author(
	id INT IDENTITY PRIMARY KEY,
	lastName NVARCHAR(100) NOT NULL,
	firstName NVARCHAR(100) NOT NULL
)

CREATE TABLE category(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL
)


CREATE TABLE book (
	id INT IDENTITY PRIMARY KEY,
	title NVARCHAR(100) NOT NULL,
	pages INT NOT NULL,
	cost DECIMAL NOT NULL,
	authorId INT FOREIGN KEY REFERENCES author(id),
	categoryId INT FOREIGN KEY REFERENCES category(id)
);

INSERT INTO author (lastName, firstName)
VALUES
  ('Tolstoy', 'Leo'),
  ('Austen', 'Jane'),
  ('Dostoevsky', 'Fyodor'),
  ('Dickens', 'Charles');

INSERT INTO category (name)
VALUES
  ('Fiction'),
  ('Mystery'),
  ('Romance'),
  ('Science Fiction');

INSERT INTO book
VALUES
  ('War and Peace', 1225, 25.99, 1, 1),
  ('Pride and Prejudice', 432, 12.50, 2, 3),
  ('Crime and Punishment', 576, 18.75, 3, 2),
  ('Great Expectations', 505, 15.99, 4, 1);

SELECT *
FROM author

SELECT *
FROM category

--------------------------
CREATE PROCEDURE pInsertAuthor
@firstName NVARCHAR(100),
@lastName NVARCHAR(100)
AS
BEGIN
    INSERT INTO author (lastName, firstName)
    VALUES (@lastName, @firstName)
END
--------------------------
CREATE PROC pGetAuthorIdByFullName
@firstName NVARCHAR(100),
@lastName NVARCHAR(100)
AS
BEGIN
    SELECT id 
	FROM author 
	WHERE firstName LIKE @firstName AND lastName LIKE @lastName
END
--------------------------
CREATE PROC pGetAuthorFullNameById
@authorId  INT
AS
BEGIN
    SELECT firstName,lastName
	FROM author 
	WHERE id = @authorId
END
--------------------------
CREATE PROCEDURE pInsertCategory
@name NVARCHAR(100)
AS
BEGIN
    INSERT INTO category (name)
    VALUES (@name)
END

--------------------------
CREATE PROCEDURE pGetCategoryNameById
@categoryId INT
AS
BEGIN
    SELECT name
	FROM category
	WHERE id = @categoryId
END

--------------------------
CREATE PROC pGetCategoryIdByName
@name NVARCHAR(100)
AS
BEGIN
    SELECT id 
	FROM category 
	WHERE name LIKE @name
END
--------------------------
CREATE PROC pInsertBook
@title NVARCHAR(100),
@pages INT,
@cost DECIMAL,
@authorId INT,
@categoryId INT
AS
BEGIN
	INSERT INTO book
	VALUES (@title,@pages,@cost,@authorId,@categoryId);
END
--------------------------
CREATE PROC pUpdateBook
@id INT,
@title NVARCHAR(100),
@pages INT,
@cost DECIMAL,
@authorId INT,
@categoryId INT
AS
BEGIN
	UPDATE book
	SET title = CASE WHEN @title IS NOT NULL THEN @title ELSE title END,
		pages = CASE WHEN @pages IS NOT NULL THEN @pages ELSE pages END,
		cost = CASE WHEN @cost IS NOT NULL THEN @cost ELSE cost END,
		authorId = CASE WHEN @authorId IS NOT NULL THEN @authorId ELSE authorId END,
		categoryId = CASE WHEN @categoryId IS NOT NULL THEN @categoryId ELSE categoryId END
	WHERE id = @id
END
--------------------------
CREATE PROC pDeleteBook
@id INT
AS
BEGIN
	DELETE FROM book
	WHERE id = @id
END
--------------------------
CREATE PROC pGetAllBook
AS
BEGIN
	SELECT *
	FROM book
END
--------------------------
CREATE PROC pGetBookById
@id INT
AS
BEGIN
	SELECT *
	FROM book
	WHERE id = @id
END