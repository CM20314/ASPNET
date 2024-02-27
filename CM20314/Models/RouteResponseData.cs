using System;
namespace CM20314.Models
{
	public class RouteResponseData
	{
		public List<NodeArcDirection> NodeArcDirections { get; set; }
        public bool Success { get; set; }
		public string ErrorMessage { get; set; }

		public RouteResponseData(List<NodeArcDirection> nodeArcDirections, bool success, string errorMessage)
		{
			NodeArcDirections = nodeArcDirections;
			Success = success;
			ErrorMessage = errorMessage;
		}
	}
}

