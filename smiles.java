public class smiles {
    /*    public static int hashmap(String s)
        {
            HashMap<String,Integer> val = new HashMap<>();
            val.put("c",4);
            val.put("C",4);
            val.put("N",3);
            val.put("O",2);
          //  val.put("Cl",1);
            val.put("H",1);
            val.put("-",1);
            val.put("=",2);
            val.put("#",3);

            return val.get(s);
        }*/
    public static void arr(String s){

        int count=0;
        String s1 = s.replaceAll("[-=#]","");
        char[] str1 = s1.toCharArray();
        int array1[] = new int[s1.length()];
        for(int i=0; i< str1.length; i++){
            if (str1[i] =='('){
                array1[count]=i - count*2;
                count++;
            }
        }
        String s2 = str1.toString().replaceAll("[()]","");
        char[] str2 = s2.toCharArray();

//ccc(O)cc
        adj(str2,array1);
    }
    //    public  static void lable(char[] a,int[] array){
//        HashMap<Integer,Character> val = new HashMap<>();
//        for (int i=0; i< a.length; i++) {
//            val.put(i+1,a[i]);
//        }
    //       adj(a,val);
   //   }
//    public static int[][] zero(int size){
//        int arr[][] = new int[size][size];
//        for(int i=0; i< size ;i++){
//            for(int j=0; j<size; j++){
//                arr[i][j]=0;
//            }
//        }
//        return arr;
//    }
    public static void adj(char[] str,int[] array){
        int size =str.length;
        int i=0 ,count =0;
        int arr[][] = new int[size][size];

        if(i == 0){
            arr[i][i+1]=1;
            i++;
        }
        for( i=1; i< size-1; i++){
            if(i==array[count]){
                arr[i-1][i+1]=1;
                arr[i][i-1]= 1;
                count++;
            }
            else {
                arr[i][i - 1] = arr[i][i + 1] = 1;
            }
        }
        if(i == size && i-1==array[count-1] ){
            arr[i][i-2]=1;
        }else{
            arr[i][i-1]=1;
        }
        print(size,arr);
    }


    public static void print(int size,int[][] arr){
        for(int i=0; i< size ;i++){
            for(int j=0; j<size; j++){
                System.out.print(arr[i][j]+" ");
            } System.out.println();
        }
    }

    public static void main(String[] args) {


//        smiles sml = new smiles();
// //       System.out.println(sml.hashmap("C"));
//
//        Scanner s = new Scanner(System.in);
//        System.out.println("Enter smiles notation");
//        String smilesNotation = s.next();
        arr("C-C#C-o-C");

    }
}
/*

class smile
{
    public static void main(String[] args) {

        Scanner get = new Scanner(System.in);

        //HASHMAP CONTAINING THE ELEMENTS--VALENCY PAIR
        HashMap<Character,Integer> eleval = new HashMap<>();
        eleval.put('c',4);
        eleval.put('N',3);
        eleval.put('O',2);
        eleval.put('H',1);

        //HASHMAP CONTAINING THE BONDS--VALUE PAIR
        HashMap<Character,Integer> bonds = new HashMap<>();
        bonds.put('-', 1);
        bonds.put('=', 2);
        bonds.put('#', 3);

        //INPUT FROM USER FOR SMILES NOTATION
        StringBuilder sb = new StringBuilder();
        System.out.println("ENTER THE SMILES NOTATION");
        sb.append(get.nextLine());

        //COVERT THE INPUT INTO A CHARACTER ARRAY
        ArrayList<Character> res = new ArrayList<>();
        for(int i=0;i<sb.length();i++)
        {
            res.add(sb.charAt(i));
        }

        //CREATING A LIST WITHOUT ANY BONDS
        ArrayList<Character> res2 = new ArrayList<>();
        for(int i=0;i<res.size();i++)
        {
            if(res.get(i)=='-'||res.get(i)=='='||res.get(i)=='#') i++;
            else{
                res2.add(res.get(i));
            }
        }
        //ADJACENCY MATRIX
        int[][] adjmat = new int[res2.size()][res2.size()];

        //CALCULATING THE ELEMENTS VALENCY
        int valency = 0;
        //int[] ans = new int[res2.size()];
        for(int i=0;i<res.size();i++)
        {
            if(i==0)
            {
                valency = (int)(eleval.get(res.get(i)) - bonds.get(res.get(i+1)));
            }
           */
/*  if(i==res.size())
            {
                valency = (int)(eleval.get(res.get(i)) - bonds.get(res.get(i-1)));
                break;
            }
            if((i!=0)&&(i!=res.size()))
            {
                valency = (int)(eleval.get(res.get(i)) - (bonds.get(res.get(i-1))+bonds.get(res.get(i+1))));
            }*//*

        }
        System.out.println(valency);

    }
}
*/
