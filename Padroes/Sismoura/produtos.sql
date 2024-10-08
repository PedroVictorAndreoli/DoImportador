select
    Produto.Codigo as ID,
    Produto.Nome as Descricao,
    Grupo_Produto.Descricao as Grupo,
    Preco_Produto as ValorVenda,
    Preco_Custo as ValorCusto,
    Quantidade as EstoqueAtual,
    Margem as MargemLucro,
    Marca.Nome as Marca,
    CASE Servico
        WHEN 'S' THEN 'Servico'
        WHEN 'N' THEN 'Produto'
    END as Tipo,
    NCM as NCM

from Produto
    left join Grupo_Produto on Grupo_Produto.Codigo = Produto.Grupo
    left join Marca on Marca.Codigo = Produto.Marca
    left join Classificacao_Fiscal on Classificacao_Fiscal.Codigo = Produto.Classificacao_Fiscal