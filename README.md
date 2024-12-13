# BallsOfDoom

Introdução:

  O projeto consiste no desenvolvimento de um jogo de corrida, em que o objetivo principal é proporcionar aos jogadores uma experiência divertida e competitiva. Este jogo está a ser desenvolvido no âmbito da unidade curricular de Inteligência Artificial Aplicada a Jogos, aplicando conceitos de programação e interatividade.


História do Jogo:

  Um grupo de exploradores encontrou as ruínas de uma antiga cidade. Ao entrarem no edifício mais chamativo dessas ruínas, os exploradores ouvem uma voz envelhecida e distorcida que diz:
“Eheheh… Curiosos exploradores… vocês sabem como é com ruínas antigas, certo? Tenho aqui a maldição perfeita para vocês... eheheh...eheh...eh…”
Em um flash de luz, os exploradores são transformados em bolas de cristal. A voz volta a falar: “Aqui nesta cidade, faziam-se corridas sempre que havia desentendimentos entre diferentes facções. Mas hoje vamos usar a pista de corrida para um pequeno desafio~. O explorador que terminar em primeiro lugar voltará a ser humano, ganhará este baú de moedas de ouro e poderá voltar ao mundo exterior. No entanto, os perdedores ficarão aqui nesta cidade esquecida, como bolas de cristal, por toda a eternidade... eheheh… Boa sorte, exploradores. Que comece a corrida… eheheh…”

  Objetivo do Jogo:
  
  O principal objetivo do jogo é simples e direto: ser o primeiro a chegar à meta. Os jogadores irão competir em tempo real e também tentar alcançar o melhor tempo para se destacarem na tabela de classificação global. Esta estrutura incentiva duas formas de desafio: vencer os outros jogadores em partidas ao vivo e melhorar continuamente o tempo de conclusão das pistas, visando posições mais altas no ranking.
  
  1. Máquina de Estados Finita (FSM): 

  Este script implementa uma máquina de estados simples para gerenciar o comportamento de uma IA de personagem. Uma máquina de estados é um modelo computacional que representa os diferentes estados que uma entidade pode assumir e as transições entre esses estados com base em condições específicas. Neste caso, a IA pode alternar entre diferentes estados, como patrulhar, rolar, pular e outros.


<p align="center">
  <img src="https://github.com/user-attachments/assets/f0fa1010-6635-4e24-8fbe-ae77aadc2edb" alt="Descrição da imagem">
</p>

  Enumeração dos Estados

Os possíveis estados que um personagem pode assumir são os seguintes:

  Parado (Idle): O personagem está em repouso, aguardando entrada ou ação.
  Rolando (Roll): O personagem inicia um movimento de rolar.
  Pulando (Jump): O personagem executa um salto.
  Sprint_Roll (Corrida com Rolar): O personagem realiza um movimento de rolamento mais rapido.
  Sprint_Roll_Back (Rolamento para trás): O personagem realiza um rolamento mais rápido para trás.


A ideia da IA Competitiva Dinâmica é criar uma experiência de jogo mais envolvente e desafiadora ao adaptar o comportamento dos bots em tempo real com base no desempenho do jogador. Esse método torna os bots "inteligentes", ajustando suas ações para criar uma competição equilibrada ou até imprevisível. Vamos detalhar as duas abordagens mencionadas:

2. Desempenho Adaptativo
   
Este componente avalia a posição e o progresso do jogador em relação aos bots e faz ajustes ao comportamento dos bots:

Como Funciona

Monitoramento do Desempenho:


![Captura de ecrã 2024-12-12 193827](https://github.com/user-attachments/assets/fa966edc-16c7-49cd-91a1-cf8ba99ca509)


Verifica a distância entre cada bot e o jogador.
Compara posições relativas em relação à linha de chegada (checkpoint ou progressão total no percurso).
Ajuste de Velocidade:

![Captura de ecrã 2024-12-12 193720](https://github.com/user-attachments/assets/71e899f1-ff01-4a1f-93b8-15ffb227505b)


Se o jogador estiver muito à frente:
Bots recebem um boost temporário na velocidade ou escolhem caminhos mais diretos (atalhos) para alcançá-lo.
Os bots podem ativar estratégias mais agressivas (por exemplo, tentar bloquear o jogador).
Se o jogador estiver muito atrás:
Bots reduzem sua velocidade ligeiramente para evitar que o jogador perca o interesse no jogo.

Gerenciamento de Dificuldade:


![Captura de ecrã 2024-12-12 194006](https://github.com/user-attachments/assets/15df2c66-3719-473e-9a2f-e20ac0612c07)

Adicione limites para evitar que bots fiquem muito rápidos ou lentos.
Use uma "zona de conforto" onde, se o jogador estiver em uma faixa intermediária, os bots mantenham o comportamento normal.


Conclusão:

  O jogo combina elementos de corrida com interação multijogador, power-ups divertidos e personalização das personagens. Este relatório documenta os principais aspetos de design e funcionalidades, e pretende servir de base para futuras melhorias e expansões no desenvolvimento deste jogo.
