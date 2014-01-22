using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class UUnitTestRunner
{
	private static void FindAndAddAllTestCases (UUnitTestSuite suite)
	{
		IEnumerable<Type> testCasesTypes = AppDomain.CurrentDomain.GetAssemblies ()
									    .Select (x => x.GetTypes ())
									    .SelectMany (x => x)
										.Where (c => !c.IsAbstract)
									    .Where (c => c.IsSubclassOf (typeof(UUnitTestCase)));

		foreach (Type testCaseType in testCasesTypes) {
			suite.AddAll (testCaseType);
		}
	}
	
	private static void ClearDebugLog ()
	{
#if UNITY_EDITOR
		Assembly assembly = Assembly.GetAssembly (typeof(SceneView));
		Type type = assembly.GetType ("UnityEditorInternal.LogEntries");
		MethodInfo method = type.GetMethod ("Clear");
		method.Invoke (new object (), null);
#endif
	}
	
#if UNITY_EDITOR
	[MenuItem("UUnit/Run All Tests %#t")]
#endif
	private static void RunAllTests ()
	{
		ClearDebugLog ();
		
		UUnitTestSuite suite = new UUnitTestSuite ();
		FindAndAddAllTestCases (suite);
		UUnitTestResult result = suite.Run ();
		
		Debug.Log (result.Summary ());
	}

}
