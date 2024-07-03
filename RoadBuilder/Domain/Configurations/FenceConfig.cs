﻿using Colossal.Serialization.Entities;

using Game.Prefabs;

using RoadBuilder.Domain.Configuration;
using RoadBuilder.Domain.Enums;
using RoadBuilder.Systems;

using System;
using System.Collections.Generic;

namespace RoadBuilder.Domain.Configurations
{
	public class FenceConfig : INetworkConfig
	{
		public ushort Version { get; set; }
		public string OriginalID { get; set; }
		public string ID { get; set; }
		public string Name { get; set; }
		public string AggregateType { get; set; }
		public string PillarPrefabName { get; set; }
		public float MaxSlopeSteepness { get; set; }
		public bool HasUndergroundWaterPipes { get; set; }
		public bool HasUndergroundElectricityCable { get; set; }
		public bool RequiresUpgradeForElectricity { get; set; }
		public RoadCategory Category { get; set; }
		public List<LaneConfig> Lanes { get; set; } = new();
		public List<NetEdgeStateInfo> EdgeStates { get; set; } = new();
		public List<NetNodeStateInfo> NodeStates { get; set; } = new();

		public void Deserialize<TReader>(TReader reader) where TReader : IReader
		{
			reader.Read(out ushort version);
			reader.Read(out string iD);
			reader.Read(out string name);
			reader.Read(out string aggregateType);
			reader.Read(out string pillarPrefabName);
			reader.Read(out float maxSlopeSteepness);
			reader.Read(out bool hasUndergroundWaterPipes);
			reader.Read(out bool hasUndergroundElectricityCable);
			reader.Read(out bool requiresUpgradeForElectricity);
			reader.Read(out int category);

			Version = version;
			ID = iD;
			Name = name;
			AggregateType = aggregateType;
			PillarPrefabName = pillarPrefabName;
			MaxSlopeSteepness = maxSlopeSteepness;
			HasUndergroundWaterPipes = hasUndergroundWaterPipes;
			HasUndergroundElectricityCable = hasUndergroundElectricityCable;
			RequiresUpgradeForElectricity = requiresUpgradeForElectricity;
			Category = (RoadCategory)category;

			reader.Read(out int laneCount);

			for (var i = 0; i < laneCount; i++)
			{
				var lane = new LaneConfig();

				reader.Read(lane);

				Lanes.Add(lane);
			}

			OriginalID = ID;
		}

		public void Serialize<TWriter>(TWriter writer) where TWriter : IWriter
		{
			writer.Write(RoadBuilderSystem.CURRENT_VERSION);
			writer.Write(ID);
			writer.Write(Name);
			writer.Write(AggregateType ?? string.Empty);
			writer.Write(PillarPrefabName ?? string.Empty);
			writer.Write(MaxSlopeSteepness);
			writer.Write(HasUndergroundWaterPipes);
			writer.Write(HasUndergroundElectricityCable);
			writer.Write(RequiresUpgradeForElectricity);
			writer.Write((int)Category);

			writer.Write(Lanes.Count);

			foreach (var lane in Lanes)
			{
				writer.Write(lane);
			}
		}

		public void ApplyVersionChanges()
		{

		}
	}
}
