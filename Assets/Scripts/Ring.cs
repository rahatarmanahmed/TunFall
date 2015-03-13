using UnityEngine;

public class Ring {
	// Where the center of bottom ring should be on the XZ plane
	public float centerX, centerY, centerZ, radius;
	public Ring(float centerX, float  centerY, float centerZ, float radius) {
		this.centerX = centerX;
		this.centerY = centerY;
		this.centerZ = centerZ;
		this.radius = radius;
	}
	public Ring(Vector3 center, float radius):
	this(center.x, center.y, center.z, radius) {
	}
	
	public static Ring SmoothStep(Ring a, Ring b, float t) {
		return new Ring(
			Mathf.SmoothStep(a.centerX, b.centerX, t),
			Mathf.Lerp(a.centerY, b.centerY, t), // Have to lerp here or it just goes linearly
			Mathf.SmoothStep(a.centerZ, b.centerZ, t),
			Mathf.SmoothStep(a.radius, b.radius, t)
			);
	}
	
//	public override bool Equals(System.Object obj) {
//		// If parameter is null return false.
//		if (obj == null)
//		{
//			return false;
//		}
//		
//		// If parameter cannot be cast to Point return false.
//		Ring o = obj as Ring;
//		if ((System.Object)o == null)
//		{
//			return false;
//		}
//		return (centerX == o.centerX
//		        && centerY == o.centerY
//		        && centerZ == o.centerZ
//		        && radius == o.radius);
//	}

	public override string ToString() {
		return string.Format("Ring[center:({0},{1},{2}), radius:{3}]", centerX, centerY, centerZ, radius);
	}
}