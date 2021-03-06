﻿using System;
using System.IO;
using System.Text;

using MiNET.Plugins;
using MiNET.Plugins.Attributes;

using MiNET;
using MiNET.Net;
using MiNET.Utils;
using MiNET.BlockEntities;
using MiNET.Blocks;
using MiNET.Entities;
using MiNET.Worlds;

namespace PRCore

{
	[Plugin(Author = "TheDiamondYT", Description = "PocketRealms Core", PluginName = "PRCore", PluginVersion = "1.0")]
    public class PRCore : Plugin
    {
		private string _basepath = MiNET.Utils.Config.GetProperty("PluginDirectory", "Plugins") + "\\PRCore";
		
		protected override void onEnable()
		{
			if (!Directory.Exists(_basepath)) Directory.CreateDirectory(_basepath);
			//TODO
		}
		
		[Command(Command = "help")]
		public void ShowHelp(Player player)
		{
			player.SendMessage("§4Help page coming soon");
		}
		
		[Command(Command = "version")]
		public void Version(Player player)
		{
			player.SendMessage("§eThis server is running MiNET §6(https://github.com/pocketrealms/minet)", type: MessageType.Chat);
		}
		
		[Command(Command = "plugins")]
		public void Plugins(Player player)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("§ePlugins: §d");
			foreach (var plugin in Context.PluginManager.Plugins)
			{
				sb.AppendLine(plugin.GetType().Name);
			}
			player.SendMessage(sb.ToString(), type: MessageType.Raw);
		}
		
		[Command(Command = "info")]
		public void Info(Player player, string otherPlayer)
		{
			if(otherPlayer == null)
			{
				player.SendMessage("§6-----[§e Server Info §6]-----");
			    player.SendMessage("§aOwners: §bMack & FuryTacticz");
			    player.SendMessage("§aTwitter: §b@PocketRealmNet");
			    player.SendMessage("§aWebsite: §bComing soon");
			    player.SendMessage("§aShop: §bComing soon");
			    player.SendMessage("§6----------------------------");
			}
			else if(otherPlayer.ToLower() == "Mack")
			{
				player.SendMessage("§6-----[ §eMack §]-----");
				player.SendMessage("§aRole: §bServer owner");
				player.SendMessage("§aTwitter: §b@HyperFuseYT");
				player.SendMessage("§aKik: §bdb_mack");
			}
			else if(otherPlayer.ToLower() == "FuryTacticz")
			{
				player.SendMessage("§6-----[ §eFuryTacticz§6 ]-----");
				player.SendMessage("§aRole: §bServer owner");
				player.SendMessage("§aTwitter: §b@FuryTacticz");
				player.SendMessage("§aKik: §bFuryTacticz");
			}
			else if(otherPlayer.ToLower() == "TheDiamondYT7")
			{
				player.SendMessage("§6-----[ §eTheDiamondYT7 §6]-----");
				player.SendMessage("§aRole: §bServer developer");
				player.SendMessage("§aTwitter: §b@TheDiamondYT");
				player.SendMessage("§aKik: §bgoldowl37");
			}
			else if(otherPlayer.ToLower() == "Asuna")
			{
				player.SendMessage("§6-----[ §eAsuna §6]-----");
				player.SendMessage("§aRole: §bServer Head-Admin");
				player.SendMessage("§aTwitter: §b@Hoersdig");
				player.SendMessage("§aKik: §bStevie121234");
			}
			else if(otherPlayer.ToLower() == "BalAnce")
			{
				player.SendMessage("§6-----[ §eBalAnce §6]-----");
				player.SendMessage("§aRole: §bServer developer");
				player.SendMessage("§aTwitter: §b@BalAncelk");
				player.SendMessage("§aKik: §bblah.jeremy");
			}
		}
		
		[Command(Command = "xp")]
		public void Experience(Player player1)
		{
			foreach (Player player in player1.Level.Players.Values)
			{
				player.Level.RelayBroadcast(new McpeSpawnExperienceOrb()
				{
					entityId = player.EntityId,
					x = (int) (player1.KnownPosition.X + 1),
					y = (int) (player1.KnownPosition.Y + 2),
					z = (int) (player1.KnownPosition.Z + 1),
					count = 10
				});
			}
		}
		
		[Command(Command = "clear")]
		public void Clear(Player player)
		{
			for (byte slot = 0; slot < 35; slot++) player.Inventory.SetInventorySlot(slot, -1); //Empty all slots.
			player.SendMessage("§cCleared all inventory items from §e" + player.Username);
		}

		[Command(Command = "clear")]
		public void Clear(Player player, Player target)
		{
			Clear(target);
		}
		
		[Command(Command = "spawnmob")]
		[Authorize(Users = "TheDiamondYT7")]
		public void SpawnMob(Player player, byte id)
		{
			Level level = player.Level;
			Mob entity = new Mob(id, level)
			{
				KnownPosition = player.KnownPosition,
			};
			entity.SpawnEntity();
		}
		
		[Command(Command = "strike")]
		public void Strike(Player player)
		{
			player.StrikeLightning();
			player.SendMessage("§aStruck you with lightning. Playing with the gods, are you?");
		}
		
		[Command(Command = "god")]
		public void NoDamage(Player player)
		{
			player.HealthManager = player.HealthManager is NoDamageHealthManager ? new HealthManager(player) : new NoDamageHealthManager(player);
			player.SendMessage("§dP§7R§8>§b God mode enabled");
		}
		
		[Command(Command = "getpos")]
		public void GetPosition(Player player){
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format("§dP§7R§8>§7 X:{0:F1} Y:{1:F1} Z:{2:F1}", player.KnownPosition.X, player.KnownPosition.Y, player.KnownPosition.Z));
			string msg = sb.ToString();
			player.SendMessage(msg, type: MessageType.raw)
		}
		
		[Command(Command = "fly")] //Needs improvement an option to disable
		public void Fly(Player player)
		{
			player.SendPackage(new McpeAdventureSettings {flags = 0x80});
			player.SendMessage("§dP§7R§8> §aFlight enabled");
		}		
		
    }
}
