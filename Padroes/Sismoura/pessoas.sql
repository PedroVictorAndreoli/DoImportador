select
    Pessoa.Codigo as ID,
    Nome as Nome,
    Nome_Fantasia as RazaoSocial,
    CASE Tipo
        WHEN 'J' then CPF
        ELSE ''
    END as CNPJ,
    CASE Tipo
        WHEN 'F' then CPF
        ELSE ''
    END as CPF,

    CASE Tipo
        WHEN 'J' then RG
        ELSE ''
    END as IE,
    CASE Tipo
        WHEN 'F' then RG
        ELSE ''
    END as RG,
    Data_Nasc as DataNascimento,
    CASE Sexo
        WHEN 'M' THEN 'Masculino'
        WHEN 'F' THEN 'Feminino'
    END as Sexo,
    CASE Tipo
        WHEN 'F' THEN 'Fisica'
        WHEN 'J' THEN 'Juridica'
    END as TipoPessoa,
    Endereco as Logradouro,
    Bairro as Bairro,
    Numero as Numero,
    Complemento as Complemento,
    Cep as CEP,
    Cidade.Cidade,
    (DDD1 + Fone_Numero) as TelefoneFixo,
    (DDD2 + Fone2_Numero) as TelefoneComercial,
    (DDD_Celular + Numero_Celular) as Celular,
    Email as Email
from Pessoa
    LEFT JOIN Cidade on Pessoa.Cidade = Cidade.Codigo