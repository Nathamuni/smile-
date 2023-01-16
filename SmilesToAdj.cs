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
        static string sb = "C-Cl(-C)-C-C"; 
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

        // public static readonly Dictionary<char,int> organicSubset = new Dictionary<String, int>()
        // {//B, C, N, O, P, S, F, Cl, Br, and I
        //     //{"B",3},{"C",4},{"O",2},{"P",5},{"P",3},{'S',6},{'S',4},{'S',2},{'F',1},{'Cl',1},{'Br',1},{'I',}
        // }

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
        }
        static void adjacencyMatrix(int size)
        {
            //int Matrix_size = graph.adjacencyList.Count;
            int Matrix_size = res.Length; 
            int[,] arr = new int[Matrix_size,Matrix_size];
            int sum1=0;
            int[,] nums = new int[size,size];
            int[] arr1 = new int[Matrix_size];
            int n=0,m=0;

            foreach(var x in graph.adjacencyList)
            {
                foreach(KeyValuePair<int, int> y in x.Value)
                {
                    arr[x.Key,y.Key] = 1;
                }
            }

            for(int i=0;i<Matrix_size;i++)
            {
                sum1=0;
                for(int j=0;j<Matrix_size;j++)
                {
                    sum1=sum1+arr[i,j];
                }
                if(sum1!=0) arr1[i]=1;
            }
            for(int i=0;i<arr1.Length;i++) Console.Write(arr1[i]+" ");
            Console.WriteLine();
            for(int i=0;i<Matrix_size;i++)
            {
                if(arr1[i]!=0)
                {
                    n=0;
                    for(int j=0;j<Matrix_size;j++)
                    {
                        if(arr1[j]!=0) 
                        {
                            nums[m,n]=arr[i,j];
                            n++;
                        }
                    }
                    m++;
                }
            }
            Console.WriteLine();
            for(int i=0;i<size;i++)
            {
                for(int j=0;j<size;j++)
                {
                    Console.Write(nums[i,j]+" ");
                }
                Console.WriteLine();
            }

            // for(int i=0;i<Matrix_size;i++)
            // {
            //     Console.WriteLine();
            //     for(int j=0;j<Matrix_size;j++)
            //     {
            //         Console.Write(arr[i,j]+" ");
            //     }
            // }
        }
        //MAIN METHOD
        static void Main(string[] args)
        {

            // CREATING A LIST WITHOUT ANY BONDS
            var res2 = new ArrayList();

            for (int i = 0; i < res.Length; i++)
            {
                if( bonds.ContainsKey(res[i]) || (res[i] == ('(')) || (res[i] == (')')) || Char.IsDigit(res[i]) || (char.IsLower(res[i])))
                {
                    continue;
                }
                else 
                {
                    // c-c
                    // res2.Add(res[i]);
                    // graph.addVertex(i);
                    // if(char.IsLower(res[i]))
                    // {
                    //     res2.Add(res[i-1]);
                    //     Console.Write(i-1+" ");
                    //     graph.addVertex(i-1);
                    // }
                    //else
                    //{
                    //}
                        res2.Add(res[i]);
                        Console.Write(i+" ");
                        graph.addVertex(i);
                }
            }    

            Console.WriteLine();        

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
                    //int idx1=i,idx2=i+1;
                    for(int j=i+1;j<res.Count();j++)
                    {
                        //idx2=j;
                        if(res[i]==res[j])//break;
                        {
                            if(char.IsLower(res[i-1])&&(char.IsLower(res[j-1]))) graph.addEdge(i-2,j-2,1);
                            else if(char.IsLower(res[i-1])) graph.addEdge(i-2,j-1,1);
                            else if(char.IsLower(res[j-1])) graph.addEdge(i-1,j-2,1);
                            else graph.addEdge(i-1,j-1,1);        
                        }
                        //graph.addEdge(i-1,j-1,1);
                    }
                }
                //CHECK/LOGIC FOR BRANCH STRUCTURES IN INPUT SMILES NOTATION
                if((res[i]==')'))
                {
                    count--;
                    if(i!=res.Length-1)
                    {
                        if(bonds.ContainsKey(res[i+1]))
                        {
                            int bond = graph.findindexvalues(i,0);
                            graph.addEdge(i+2,arr[count],bond);
                        }
                    }
                }
                if((res[i]=='(')&&(res[i-1]!=')'))
                {
                    if(char.IsLower(res[i-1]))
                    {
                        arr[count] = i-2;
                    }
                    else 
                    {
                        arr[count] = i-1;
                    }
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
                    // graph.addEdge(i-1,i+2,bond);
                    if(char.IsLower(res[i-1]))
                    {
                        graph.addEdge(i-2,i+2,bond);
                    }
                    else
                    {
                        graph.addEdge(i-1,i+2,bond);
                    }
                }
                //CHECK/LOGIC FOR BONDS IN INPUT SMILES NOTATION
                if((bonds.ContainsKey(res[i]))&&(res[i-1]!=('('))&&(res[i-1]!=(')')))
                {
                    if(char.IsDigit(res[i-1]))
                    {
                        if(char.IsLower(res[i-2]))
                        {
                            graph.addEdge(i-3,i+1,bonds[res[i]]);
                        }
                        else
                        {
                            graph.addEdge(i-2,i+1,bonds[res[i]]);
                        }
                        //graph.addEdge(i-2,i+1,bonds[res[i]]);
                    }
                    else
                    {
                        //graph.addEdge(i-1,i+1,bonds[res[i]]);
                        if(char.IsLower(res[i-1]))
                        {
                            graph.addEdge(i-2,i+1,bonds[res[i]]);
                        }
                        else
                        {
                            graph.addEdge(i-1,i+1,bonds[res[i]]);
                        }
                    }
                }
            }

            //IMPLICIT HYDROGEN INITIALIZATION
            //implicitHydrogenIndex();
            //ADJACENCY MATRIX GENERATION
            adjacencyMatrix(res2.Count);

            //Console.WriteLine();

            //PRINTING THE GRAPH
            // foreach (var x in graph.adjacencyList)
            // { 
            //     Console.WriteLine(x.Key);
            //     foreach (KeyValuePair<int, int> y in x.Value)
            //     {
            //         Console.WriteLine("vertex " + x.Key + " ---> " + y.Key + ": "+y.Value);
            //     }
            // }
        }
    }
}

