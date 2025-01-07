# BallsOfDoom

Introdução:

  O projeto consiste no desenvolvimento de um jogo de corrida, em que o objetivo principal é proporcionar aos jogadores uma experiência divertida e competitiva. 


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


3.Navegação com Waypoints


A IA calcula a direção para o próximo waypoint utilizando a diferença entre a posição atual do bot e a posição do waypoint alvo. Essa direção é normalizada para garantir que o movimento seja proporcional, sem afetar a velocidade.
O método MoveInDirection(direction) é chamado para movimentar o bot na direção calculada.


Chegada ao Próximo Waypoint


A distância entre o bot e o waypoint alvo é verificada utilizando o método Vector3.Distance. Quando a distância é menor ou igual a um valor limiar (waypointThreshold), o bot avança para o próximo waypoint incrementando o índice (currentWaypointIndex).


![Captura_de_ecra_2024-12-13_205514](https://github.com/user-attachments/assets/3f45285a-367c-4e77-ace7-3bdef6b833f6)


Verificação de Fim da Lista de Waypoints


Se o índice atual do waypoint (currentWaypointIndex) ultrapassar o número total de waypoints, o método detecta que o último ponto foi alcançado e interrompe o processo de movimentação.


Detecção de Obstáculos


Antes de prosseguir para o próximo waypoint, o método verifica se há obstáculos nas proximidades utilizando IsObstacleDetected().
Caso um obstáculo seja detectado, o estado do bot é alterado para State.AvoidingObstacle, permitindo que ele mude seu comportamento para contornar ou lidar com o obstáculo.


![Captura_de_ecra_2024-12-13_205420](https://github.com/user-attachments/assets/174e68ff-5f62-4349-a17d-8ff126668f10)


Movimentação em Direção aos Waypoints


A IA calcula a direção para o próximo waypoint utilizando a diferença entre a posição atual do bot e a posição do waypoint alvo. Essa direção é normalizada para garantir que o movimento seja proporcional, sem afetar a velocidade. O método MoveInDirection(direction) é responsável por mover o bot nessa direção e ajustar sua rotação para que continue olhando na direção de movimento.


![Captura_de_ecra_2024-12-13_210944](https://github.com/user-attachments/assets/af4effb4-cad6-4f9a-ad1f-df3161c031ed)


Conclusão:


  O jogo combina elementos de corrida com interação multijogador. Este relatório documenta os principais aspetos de design e funcionalidades, e pretende servir de base para futuras melhorias e expansões no desenvolvimento deste jogo.
