# Teste Técnico

## Informações gerais

- O repositório ja está com um banco de dados SQLite (`dbcontribuir.db`) pronto para uso.

- Foi criado teste unitário para a função `GetResumoPorContribuinte` (resumo de debitos).

- Foi utilizado log de `Warning` e `Error` em duas funções do servico de contribuintes.

## Endpoints

### POST /api/contribuintes

- Estrutura JSON

```json
{
  "nome": "Joao da Silva",
  "cpfCnpj": "12345678901"
}
```

#### Exemplo das Respostas esperadas

- 201 Created

```json
{
  "id": "7e49b1d1-7c83-4f7c-9b1b-9f6e2d9d7a22",
  "nome": "Joao da Silva",
  "cpfCnpj": "12345678901",
  "dataCriacao": "2026-04-29T10:30:00"
}
```

- 400 Bad Request

```json
{
  "message": "Contribuinte com este CPF/CNPJ já existe",
  "errors": ["CPF/CNPJ duplicado"]
}
```

### GET /api/contribuintes/{id}

#### Exemplo da Resposta Esperada

- 200 Ok

```json
{
  "id": "7e49b1d1-7c83-4f7c-9b1b-9f6e2d9d7a22",
  "nome": "Joao da Silva",
  "quantidadeTotalDebitos": 3,
  "totalEmAberto": 150.0,
  "quantidadeDebitosVencidos": 1,
  "totalVencido": 50.0
}
```

- 404 Not Found

```json
{
  "message": "Contribuinte não encontrado"
}
```

### POST /api/debitos

- Estrutura JSON

```json
{
  "contribuinteId": "7e49b1d1-7c83-4f7c-9b1b-9f6e2d9d7a22",
  "valor": 100.5,
  "dataPagamento": null,
  "dataVencimento": "2026-05-10T00:00:00"
}
```

#### Exemplo da Resposta esperada

- 201 Created

```json
{
  "contribuinteId": "7e49b1d1-7c83-4f7c-9b1b-9f6e2d9d7a22",
  "valor": 100.5,
  "dataPagamento": null,
  "dataVencimento": "2026-05-10T00:00:00"
}
```

- 400 Bad Request

```json
{
  "message": "Erro ao criar débito",
  "errors": ["detalhe do erro"]
}
```

- 404 Not Found

```json
{
  "message": "Contribuinte não encontrado",
  "errors": ["ID do contribuinte inválido"]
}
```
