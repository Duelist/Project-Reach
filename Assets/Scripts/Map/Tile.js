#pragma strict
@script ExecuteInEditMode()
class Tile extends MonoBehaviour
{
	public var position : Vector2;
	public var pastState : boolean;
	public var hasSelector : boolean;
}