import java.util.ArrayList;

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
        System.out.println("The adj matrix4 : "+s1);
        char[] str1 = s1.toCharArray();

        ArrayList<Integer> array1 = new ArrayList<Integer>();
        for(int i=0; i< str1.length; i++){
            if (str1[i] =='('){
              //  array1[count]=i - count*2;
                array1.add(i - count*2);
                count++;
            }
        }
        ArrayList<Character> ar = new ArrayList<>();

        for(int j=0;j<str1.length;j++)
        {
            if(str1[j]=='('|| str1[j]==')') continue;
            else ar.add(str1[j]);
        }


//ccc(O)cc
        adj(ar,array1);
    }

    public static void adj(ArrayList<Character> ar,ArrayList<Integer> array){
        int size =ar.size();
        int i=0 ,count =0;
        int arr[][] = new int[size][size];

        if(i == 0){
            arr[i][i+1]=1;
            i++;
        }
        for( i=1; i< size-1; i++){
            if(array.contains(i)){
                arr[i-1][i+1]=1;
                arr[i][i-1]= 1;
                count++;
            }
            else if(array.contains(i-1))
            {
                arr[i][i-2]=1;
                arr[i][i+1]=1;
            }
            else {
                arr[i][i - 1] = arr[i][i + 1] = 1;
            }
        }

        if(array.size() !=0 && i == size-1 && i-1==array.get(count-1)) {
            arr[i][i - 2] = 1;

        }else {
            arr[i][i - 1] = 1;
        }
        print(size,arr,ar);
    }


    public static void print(int size,int[][] arr,ArrayList<Character> ar){
        System.out.println("  "+ar);
        for(int i=0; i< size ;i++){
            System.out.print(ar.get(i)+"| ");
            for(int j=0; j<size; j++){
                System.out.print(arr[i][j]+"  ");
            } System.out.println();
        }
    }

    public static void main(String[] args) {
        arr("C=(c)-c-(o)-c");

    }
}
