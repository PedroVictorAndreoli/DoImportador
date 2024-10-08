select
    Animais_Cadastro.Codigo as ID,
    Nome_Animal as Nome,
    Pessoa.Codigo as IDDono,
    Pessoa.Nome as NomeDono,
    Animais_Raca.Descricao as Raca,
    Animais_Especie.Nome as Especie,
    Data_Nasc as DataNascimento,
    CASE Animais_Cadastro.Sexo
        WHEN 'M' THEN 'Masculino'
        WHEN 'F' THEN 'Feminino'
    END as Sexo,
    Animais_Tipo_Pelagem.Descricao as Pelagem,
    Peso as Peso,
    Animais_Pelagem.Descricao as Cor
from Animais_Cadastro
    Inner join Pessoa on Pessoa.Codigo = Animais_Cadastro.Cod_Proprietario
    left join Animais_Raca on Animais_Raca.Codigo = Animais_Cadastro.Cod_Raca
    left join Animais_Especie on Animais_Especie.Codigo = Animais_Cadastro.Cod_Especie
    left join Animais_Pelagem on Animais_Pelagem.Codigo = Animais_Cadastro.Pelagem
    left join Animais_Tipo_Pelagem on Animais_Tipo_Pelagem.Codigo = Animais_Cadastro.Tipo_Pelagem