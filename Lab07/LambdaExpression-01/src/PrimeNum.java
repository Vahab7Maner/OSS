import java.util.*;

public class PrimeNum {
	public static void main(String[] args) {
		int no;
		System.out.println("ENTER NUMBER: ");
		Scanner sc=new Scanner(System.in);
		no=sc.nextInt();
	
			Runnable r1 = ()->{
				System.out.println("\n\n Prime no are :");
				for(int j=2;j<=no;j++)
				{
				int count=0;
				for(int i=1;i<=j;i++)
				{
				   if(j%i==0)
				   {
				        count++;        
				   }
				}
				if(count==2)
				       System.out.print(j+"  ");  
				}
				};
			new Thread(r1).start();
			sc.close();
	}

}