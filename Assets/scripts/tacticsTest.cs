using UnityEngine;
using System.Collections;

public class tacticsTest : UUnitTestCase {

	[UUnitTest]
	public void run() {
		UUnitAssert.NotNull( tacticsRule.get() );
		UUnitAssert.Equals(true, tacticsRule.get().makeTile());
	}
}
