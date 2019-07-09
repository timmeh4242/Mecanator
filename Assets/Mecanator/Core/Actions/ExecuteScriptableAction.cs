public class ExecuteScriptableAction : StateMachineAction
{
	public ScriptableAction ScriptableAction;

	public override void Execute (StateMachineActionObject smao)
	{
		base.Execute (smao);

		ScriptableAction.Execute (smao);
	}
}
