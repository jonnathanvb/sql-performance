CREATE TABLE Pessoa
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(255) NOT NULL,
    Telefone VARCHAR(20),
    Logradouro VARCHAR(255),
    Uf VARCHAR(2),
    Ano INT,
    Mes INT,
    Dia INT
);