namespace dsaprojekt.Graphs
{
	public class BFS
	{
		//procedure BFS(Graph, source):
		//create a queue Q
		//enqueue source onto Q
		//mark source
		//while Q is not empty:
		//dequeue an item from Q into v
		//for each edge e incident on v in Graph:
		//let w be the other end of e
		//if w is not marked:
		//mark w
		//enqueue w onto Q


		//Forklaring af algoritmen

		//1. Opret en queue og placer en fiktiv kant gående fra og til start

		//2. Marker startnoden som besøgt

		//3. Fjern og anvend den første kant fra jeres queue

		//4. Hvis en slutnode ønskes fundet testes det om destinationen er
		//slutnoden
		//• Algoritmen afsluttes hvis dette er tilfældet
		//• Stien findes ved at følge parentes fra destinationen tilbage til start

		//5. Hvis slutnoden ikke er fundet tilføjes alle nabokanter med
		//unvisited noder til jeres queue, hvis der ingen noder er
		//backtrackes der.
		//• Noderne markeres som visited.
		//• Parent noden noteres

		//6. Algoritmen gentages fra step 3
	}
}
