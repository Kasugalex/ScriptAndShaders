using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CircleFillGrid : MonoBehaviour {

	[SerializeField]
	private Transform target;

	[SerializeField]
	[Range(1,100)]
	private int gridSize = 100;

	[SerializeField]
	[Range(1,5)]
	private int cellSize = 1;

	[SerializeField]
	private float radius = 10;

	private struct PosStruct{
		public Vector3 cellPos;
		public Vector2Int gridPos;
	}

	private List<Vector2> GetCubes(PosStruct centerPosS){
		List<Vector2> ret = new List<Vector2>();

		Vector2Int gridPos = centerPosS.gridPos;
		Vector2 cellPos = centerPosS.cellPos;
		int diffGrid = GetCenter(new Vector2(cellPos.x + radius,cellPos.y)).gridPos.x - gridPos.x;

		//这里可以镜像x 和 y 的坐标就能找到其余三个四分圆的点
		// for(int i = gridPos.x + 1;i <= gridPos.x + diffGrid;i++){
		// 	for(int j = gridPos.y + 1;j <= gridPos.y + diffGrid;j++){
		// 		Vector2 cubeGridPos = new Vector2(i,j);
		// 		if(CheckAtTheEdge(cubeGridPos,centerPosS)){
		// 			ret.Add(new Vector2(i,j));
		// 		}
		// 	}
		// }

		//也可以直接暴力遍历矩形内所有点
		for(int i = gridPos.x - diffGrid;i <= gridPos.x + diffGrid;i++){
			for(int j = gridPos.y - diffGrid;j <= gridPos.y + diffGrid;j++){
				Vector2 cubeGridPos = new Vector2(i,j);
				if(CheckAtTheEdge(cubeGridPos,centerPosS)){
					ret.Add(new Vector2(i,j));
				}
			}
		}

		return ret;
	}

	private bool CheckAtTheEdge(Vector2 gridPos,PosStruct centerPosS){

		if(gridPos.x < 0 || gridPos.x >= gridSize || gridPos.y < 0 || gridPos.y >= gridSize) return false;
		float halfSize  = cellSize * 0.5f;
		Vector2 cellPos = ConvertGridPosToCellPos(gridPos);
		// print("真是位置" + cellPos + "网格位置" + gridPos);
		Vector2 lowerLeft = cellPos - Vector2.one * halfSize;
		Vector2 upperRight = cellPos + Vector2.one * halfSize;
		
		Vector2 upperLeft = new Vector2(lowerLeft.x,upperRight.y);
		Vector2 lowerRight = new Vector2(upperRight.x,lowerLeft.y);
		

		Vector2[] pos = new Vector2[4];
		pos[0]	= lowerLeft;
		pos[1]	= upperRight;
		pos[2]	= upperLeft;
		pos[3]	= lowerRight;

		//inside和outside相同时，则刚好在圆上
		bool inside = false,outside = false;
		for(int i = 0;i<4;i++){
			int corner = CheckOnTheCircle(pos[i],centerPosS);
			if(corner == 0){
				inside = true;
			}else if(corner == 2){
				outside = true;
			}
		}

		if(inside == outside){
			Gizmos.DrawCube(cellPos,Vector2.one * cellSize);
		}

		return inside == outside;
	}

	private int CheckOnTheCircle(Vector2 point,PosStruct centerPosS){
		Vector2 targetPos = centerPosS.cellPos;
		float xDiff = targetPos.x - point.x;
		float yDiff = targetPos.y - point.y;

		float rSqr  = radius * radius;

		float value = xDiff * xDiff + yDiff * yDiff;
		if(value < rSqr){
			return 0;
		}else if(value == rSqr){
			return 1;
		}else{
			return 2;
		}
	}

	private Vector2 ConvertGridPosToCellPos(Vector2 gridPos){
		float halfSize = cellSize * 0.5f;
		return new Vector2((gridPos.x + 1) * cellSize - halfSize ,(gridPos.y + 1) * cellSize - halfSize);
	}

	private PosStruct GetCenter(Vector3 pos){

		Vector3 targetPos = pos;
		int x = (int)targetPos.x / cellSize + 1;
		int y = (int)targetPos.y / cellSize + 1;

		float halfSize = cellSize * 0.5f;

		Vector3 cellPos = new Vector3(x * cellSize - halfSize,y * cellSize - halfSize,0);
		PosStruct posS;
		posS.cellPos = cellPos;
		posS.gridPos = new Vector2Int(x - 1,y - 1);
		return posS;
	}
	

	private void OnDrawGizmos()
	{
		cellSize = cellSize <= 0 ? 1 : cellSize;
		Gizmos.color = Color.gray;
		Vector3 origin = transform.position;
		float halfSize = cellSize * 0.5f;
		Vector3 cubeSize = new Vector3(cellSize,cellSize,0);
		// float quaterSize = halfSize * 0.5f;
		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
			{
				Vector3 pos = origin + new Vector3((i + 1) * cellSize - halfSize,(j + 1) * cellSize - halfSize,0);
				Gizmos.DrawWireCube(pos,cubeSize);
			}
		}
		
		PosStruct posS 		= GetCenter(target.position);
		Vector3 center		= posS.cellPos;
		
		Gizmos.color = Color.red;
		List<Vector2> cubePos = GetCubes(posS);

		Gizmos.color 		= Color.green;
		Gizmos.DrawWireSphere(center,radius);
	}
}
