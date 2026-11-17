# Especificação verificável — EV-01

## Necessidade

A instituição precisa permitir que usuários da categoria Visitante reservem recursos, porém sujeitos a restrições mais rígidas do que as aplicadas a alunos e professores. Essa funcionalidade deve ser acrescentada sem alterar o algoritmo central do ServicoReservas, demonstrando o uso de polimorfismo para variar o comportamento por categoria.

## Requisitos funcionais da evolução

Um visitante deve conseguir solicitar a reserva de um recurso pelo mesmo fluxo já existente, ou seja, pelo método Solicitar do ServicoReservas. A decisão de permitir ou negar essa reserva deve ser tomada por uma política própria, a PoliticaReservaVisitante, que implementa a interface IPoliticaReserva. Essa nova política deve ser registrada no CatalogoPoliticasReserva sem que o serviço precise ser modificado para reconhecê-la.

## Regras de negócio

A reserva de um visitante pode durar no máximo uma hora. O visitante só pode reservar recursos do tipo Sala ou Projetor. O recurso escolhido precisa estar ativo. Além dessas restrições específicas, continuam valendo as regras gerais do domínio: o mesmo recurso não pode ter duas reservas bloqueadoras com períodos sobrepostos, e o ciclo de vida da reserva permanece controlado pelas transições de estado já definidas.

## Invariantes preservadas

Toda reserva continua nascendo no estado Solicitada. O período continua exigindo que o início seja anterior ao fim. A composição entre Reserva e PeriodoReserva permanece obrigatória e não pode ser substituída externamente. A coleção interna da agenda continua protegida contra alteração direta de fora. O estado da reserva segue só podendo mudar pelos comportamentos da própria Reserva.

## Critérios de aceitação

São casos de sucesso esperados: um visitante reservando uma sala ativa por uma hora deve ser permitido; um visitante reservando um projetor ativo por trinta minutos também deve ser permitido.

São casos de rejeição esperados: um visitante tentando reservar por mais de uma hora deve ser negado; um visitante tentando reservar um recurso que não seja sala nem projetor, como um laboratório ou um notebook, deve ser negado; um visitante tentando reservar um recurso inativo deve ser negado; e um visitante tentando reservar um período que conflita com uma reserva bloqueadora já existente do mesmo recurso deve ser negado com conflito de reserva.

## Fora de escopo

Não fazem parte desta evolução a persistência em banco de dados, a criação de interface gráfica ou API HTTP, a inclusão de novas categorias de usuário além de Visitante, nem qualquer alteração nas regras já existentes de aluno e professor.

## Elementos da solução afetados

A evolução cria a classe PoliticaReservaVisitante, no projeto de domínio, dentro da pasta de políticas. Também altera o Program.cs do console apenas para registrar a nova política no catálogo. Nenhuma alteração foi necessária no ServicoReservas, no CatalogoPoliticasReserva ou nas demais políticas, o que evidencia a aplicação do princípio aberto para extensão e fechado para modificação.
