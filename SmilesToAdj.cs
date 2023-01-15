using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace xyz
{
    class WeightedGraph
    {
        //INPUT SMILES
        static string sb = "C-C(-C(=O)-O)-C"; 
        //CONVERTING THE INPUT SMILES STRING INTO CHARACTER ARRAY-->RES
        //FOR EASY TRAVERSAL
        static char[] res = sb.ToCharArray();
        //ADJACENCY LIST CONTAINING FOR GRAPH NODES CONNECTIONS
        public Dictionary<int, Dictionary<int, int>> adjacencyList;
        
        //CREATING OBJECT 
        static WeightedGraph graph = new WeightedGraph();

        public Dictionary<int,int> HydrogenCount;
        static int[] implicitHydrogen = new int[sb.Length];
        //readonly
        public static readonly Dictionary<char,int> Elements = new Dictionary<char, int>()
        {
            {'C',4},{'N',3},{'O',2},{'H',1}
        };
        public static readonly Dictionary<char,int> bonds = new Dictionary<char, int>()
        {
            {'-',1},{'=',2},{'#',3},{'$', 4},{'.', 0}
            // {':',1.5},
        };

        //CONSTRUCTOR 
        public WeightedGraph()
        {
            this.adjacencyList = new Dictionary<int , Dictionary<int, int>>(){};
            this.HydrogenCount = new Dictionary<int, int>(){};
        }
        //VERTEX CREATION-->Add new vertex
        public void addVertex(int vertex) 
        {
            if (!this.adjacencyList.ContainsKey(vertex))
            {
                this.adjacencyList.Add(vertex, new Dictionary<int, int>());
            }
            else
            {
                Console.WriteLine("this vertex is in use");
            }
        }
        //CREATION OF EDGES BETWEEN NODES--> New edge between 2 vertices
        public void addEdge(int v1, int v2, int weight) 
        {
            if (this.adjacencyList.ContainsKey(v1) && this.adjacencyList.ContainsKey(v2))
            {
                this.adjacencyList[v1].Add(v2, weight);
                this.adjacencyList[v2].Add(v1, weight);
            }
            else
            {
                Console.WriteLine("Error: Vertex does not exist");
            }
        }
        //findindexvalues-->USED FOR FINDING THE BOND BEFORE THE GIVEN INDEX
        public int findindexvalues(int idx,int logic)
        {
            int bond=0;
            if(logic==1) bond = bonds[res[idx-1]];
            if(logic==0) bond = bonds[res[idx+1]];
            return bond;
        }
        static void implicitHydrogenIndex()
        {
            int valency=0;
            int bondsCount=0;
            int element= 0;
            foreach (var x in graph.adjacencyList)
            {
                valency=0;
                bondsCount=0;
                element=Elements[res[x.Key]];
                foreach(KeyValuePair<int, int> y in x.Value)
                {
                    bondsCount+=y.Value;
                }
                valency=element-bondsCount;
                graph.HydrogenCount.Add(x.Key,valency);
            }
            foreach(KeyValuePair<int, int> ele in graph.HydrogenCount)
            {
                Console.WriteLine(ele.Key +"-->"+ ele.Value);
            }
            //Console.WriteLine(graph.adjacencyList.Count);
        }
        static void adjacencyMatrix()
        {
            //int Matrix_size = graph.adjacencyList.Count;
            int Matrix_size = res.Length; 
            int[,] arr = new int[Matrix_size,Matrix_size];
            foreach(var x in graph.adjacencyList)
            {
                foreach(KeyValuePair<int, int> y in x.Value)
                {
                    arr[x.Key,y.Key] = 1;
                }
            }
            for(int i=0;i<Matrix_size;i++)
            {
                Console.WriteLine();
                for(int j=0;j<Matrix_size;j++)
                {
                    Console.Write(arr[i,j]+" ");
                }
            }
        }
        //MAIN METHOD
        static void Main(string[] args)
        {

            // CREATING A LIST WITHOUT ANY BONDS
            var res2 = new ArrayList();

            for (int i = 0; i < res.Length; i++)
            {
                if( bonds.ContainsKey(res[i]) || (res[i] == ('(')) || (res[i] == (')')) || Char.IsDigit(res[i]))
                {
                    continue;
                }
                else 
                {
                    res2.Add(res[i]);
                    graph.addVertex(i);
                }
            }            

            //PRINT THE LIST RES-->CONTAINS INPUT SMILES IN CHARACTER ARRAY FORM
            for(int i=0;i<res.Length;i++)
            {
                Console.Write(res[i]+" ");
            }

            Console.WriteLine();

            //PRINT THE LIST RES2-->CONTAINS THE ELEMENTS IN THE INPUT SMILES
            for(int i=0;i<res2.Count;i++){
                Console.Write(res2[i]+" ");
            }

            //DUMMY-->used for branch calcs
            int[] arr = new int[100];
            int count = 0;     

            //TRAVERSE THROUGH THE CHARACTER ARRAY AND INITIATE THE EDGE-WEIGHTS 
            for(int i=0;i<res.Count();i++)
            {   
                //CHECK/LOGIC FOR RING STRUCTURES IN THE INPUT SMILES
                if(char.IsDigit(res[i]))
                {
                    for(int j=i+1;j<res.Count();j++)
                    {
                        if(res[i]==res[j]) graph.addEdge(i-1,j-1,1);
                    }
                }
                //CHECK/LOGIC FOR BRANCH STRUCTURES IN INPUT SMILES
                if((res[i]==')'))
                {
                    count--;
                    if(i!=res.Length-1)
                    {
                        if((res[i+1]=='-')||(res[i+1]=='=' )||(res[i+1]=='#'))
                        {
                        int bond = graph.findindexvalues(i,0);
                        graph.addEdge(i+2,arr[count],bond);
                        }
                    }
                }
                if((res[i]=='(')&&(res[i-1]!=')'))
                {
                    arr[count] = i-1;
                    count++;
                } 
                if((res[i]=='(')&&(res[i-1]==')'))
                {
                    int bond = graph.findindexvalues(i,0);
                    graph.addEdge(arr[count++],i+2,bond);
                }
                else if((res[i]=='(')&&(res[i-1]!=')'))
                {
                    int bond = graph.findindexvalues(i,0);
                    graph.addEdge(i-1,i+2,bond);
                }
                //CHECK/LOGIC FOR BONDS IN INPUT SMILES
                if((bonds.ContainsKey(res[i]))&&(res[i-1]!=('('))&&(res[i-1]!=(')')))
                {
                    if(char.IsDigit(res[i-1]))
                    {
                        graph.addEdge(i-2,i+1,bonds[res[i]]);
                    }
                    else
                    {
                        graph.addEdge(i-1,i+1,bonds[res[i]]);
                    }
                }
            }

            //IMPLICIT HYDROGEN INITIALIZATION
            implicitHydrogenIndex();
            //ADJACENCY MATRIX GENERATION
            adjacencyMatrix();

            Console.WriteLine();

            //PRINTING THE GRAPH
            foreach (var x in graph.adjacencyList)
            {
                foreach (KeyValuePair<int, int> y in x.Value)
                {
                    Console.WriteLine("vertex " + x.Key + " ---> " + y.Key + ": "+y.Value);
                }
            }
        }
    }
}
