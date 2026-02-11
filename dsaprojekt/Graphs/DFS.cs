namespace dsaprojekt.Graphs
{
	public class DFS
	{
		// Pseudo kode fra undervisning

		// procedure DFS-iterative(G,v):
		// let S be a stack
		// S.push(v)
		//while S is not empty
		//v ← S.pop()
		//if v is not labeled as discovered:
		//label v as discovered
		//for all edges from v to w in G.adjacentEdges(v) do
		//S.push(w)


		//Forklaring af algoritmen

		//1. Opret en stack og placer en fiktiv kant gående fra og til start

		//2. Fjern og anvend top kanten fra jeres stack

		//3. Noter parent til noden, start er parent til sig selv(kan også
		//gøres i step 6, dette kan være lettere)

		//4. Noter destinationsnoden som besøgt

		//5. Hvis en slutnode ønskes fundet testes det om destinationen er
		//slutnoden
		//• Algoritmen afsluttes hvis dette er tilfældet
		//• Stien findes ved at følge parentes fra destinationen tilbage til start

		//6. Hvis slutnoden ikke er fundet tilføjes alle nabokanter med
		//ubesøgte noder til stacken, hvis der ingen noder er backtrackes
		//der

		//7. Algoritmen gentages fra punkt 2 til målet er fundet
	}
}
