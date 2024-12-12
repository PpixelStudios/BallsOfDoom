# BallsOfDoom

Introdução:

  O projeto consiste no desenvolvimento de um jogo de corrida, em que o objetivo principal é proporcionar aos jogadores uma experiência divertida e competitiva. Este jogo está a ser desenvolvido no âmbito da unidade curricular de Inteligência Artificial Aplicada a Jogos, aplicando conceitos de programação e interatividade.


História do Jogo:

  Um grupo de exploradores encontrou as ruínas de uma antiga cidade. Ao entrarem no edifício mais chamativo dessas ruínas, os exploradores ouvem uma voz envelhecida e distorcida que diz:
“Eheheh… Curiosos exploradores… vocês sabem como é com ruínas antigas, certo? Tenho aqui a maldição perfeita para vocês... eheheh...eheh...eh…”
Em um flash de luz, os exploradores são transformados em bolas de cristal. A voz volta a falar: “Aqui nesta cidade, faziam-se corridas sempre que havia desentendimentos entre diferentes facções. Mas hoje vamos usar a pista de corrida para um pequeno desafio~. O explorador que terminar em primeiro lugar voltará a ser humano, ganhará este baú de moedas de ouro e poderá voltar ao mundo exterior. No entanto, os perdedores ficarão aqui nesta cidade esquecida, como bolas de cristal, por toda a eternidade... eheheh… Boa sorte, exploradores. Que comece a corrida… eheheh…”

  Objetivo do Jogo:
  
  O principal objetivo do jogo é simples e direto: ser o primeiro a chegar à meta. Os jogadores irão competir em tempo real e também tentar alcançar o melhor tempo para se destacarem na tabela de classificação global. Esta estrutura incentiva duas formas de desafio: vencer os outros jogadores em partidas ao vivo e melhorar continuamente o tempo de conclusão das pistas, visando posições mais altas no ranking.
  
  Máquina de Estados Finita (FSM): 

  Este script implementa uma máquina de estados simples para gerenciar o comportamento de uma IA de personagem. Uma máquina de estados é um modelo computacional que representa os diferentes estados que uma entidade pode assumir e as transições entre esses estados com base em condições específicas. Neste caso, a IA pode alternar entre diferentes estados, como patrulhar, rolar, pular e outros.
![Captura de ecrã 2024-12-12 133432](https://github.com/user-attachments/assets/f0fa1010-6635-4e24-8fbe-ae77aadc2edb)


  Enumeração dos Estados

Os possíveis estados que um personagem pode assumir são os seguintes:

    Parado (Idle): O personagem está em repouso, aguardando entrada ou ação.
    Rolando (Roll): O personagem realiza um movimento de rolar para esquivar ou deslocar-se rapidamente.
    Pulando (Jump): O personagem executa um salto.
    Sprint_Roll (Corrida com Rolar): O personagem realiza um movimento de rolamento mais rapido.
    Sprint_Roll_Back (Rolamento para trás): O personagem realiza um rolamento mais rápido para trás.

    ![Captura de ecrã 2024-12-12 133432](https://github.com/user-attachments/assets/9ceee633-0e7b-4fa0-8567-90761ba43899)





Conclusão:

  O jogo combina elementos de corrida com interação multijogador, power-ups divertidos e personalização das personagens. Este relatório documenta os principais aspetos de design e funcionalidades, e pretende servir de base para futuras melhorias e expansões no desenvolvimento deste jogo.
