import java.util.*;

public class SortingString {
public static void main(String[] args) {
	
	Set<String> words1 = new TreeSet<>((String o1, String o2)-> {
		return o1.length()-o2.length();
	});
	
	words1.add("vahab");
	words1.add("sourabh");
	words1.add("sangram");
	words1.add("vyanktesh");
	words1.add("amol");
	
	for(String s : words1)
		System.out.println(s);
}
}
