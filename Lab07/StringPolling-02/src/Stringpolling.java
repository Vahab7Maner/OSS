
public class Stringpolling {
	
	public static void main(String[] args) {
	
		String s1 = new String("vahab");
		String s2 = "Vahab";
		System.out.println("Reference Equality: " +(s1==s2));
		System.out.println("Content Equality :"+s1.equals(s2));
		
		String s3 = new String("vahab");
		String s4 = "Vahab";
		System.out.println("Reference Equality: " +(s1==s3));
		System.out.println("Content Equality :"+s4.equals(s2));
	
	}

}
