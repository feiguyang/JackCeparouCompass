﻿using Turbo.Plugins.Jack.Items;

namespace Turbo.Plugins.Jack.Customize
{
    using Turbo.Plugins.Default;
    using Turbo.Plugins.Jack.TextToSpeech;

    public class SoundAlertCustomizer : BasePlugin, ICustomizer
    {
        public SoundAlertCustomizer()
        {
            Enabled = true;
        }

        public void Customize()
        {
            Hud.RunOnPlugin<GoblinPlugin>(plugin =>
            {
                plugin.EnableSpeak = false; //just in case the default change

                foreach (var deco in plugin.AllGoblinDecorators())
                {
                    deco.Add(SoundAlertFactory.Create<IMonster>(Hud, (monster) => monster.SnoMonster.NameLocalized));
                }

                plugin.DefaultGoblinDecorator.Add(SoundAlertFactory.Create<IActor>(Hud, (actor) => "goblin"));
                plugin.PortalDecorator.Add(SoundAlertFactory.Create<IActor>(Hud, (actor) => "portal"));
            });

            Hud.RunOnPlugin<StandardMonsterPlugin>(plugin =>
            {
                plugin.BossDecorator.Add(SoundAlertFactory.Create<IActor>(Hud));
            });

            Hud.RunOnPlugin<ShrinePlugin>(plugin =>
            {
                plugin.AllShrineDecorator.Add(SoundAlertFactory.Create<IShrine>(Hud, (shrine) => shrine.SnoActor.NameLocalized));
                plugin.PoolOfReflectionDecorator.Add(SoundAlertFactory.Create<IShrine>(Hud, (shrine) => "pool"));
            });

            Hud.RunOnPlugin<RamaladniDropFixPlugin>(plugin =>
            {
                plugin.SoundAlert.TextFunc = (item) => "OMAGAD a gift";
            });

            Hud.RunOnPlugin<Jack.Items.ItemDropSoundAlertPlugin>(plugin =>
            {
                // legendaries
                plugin.Legendary = true;
                plugin.AncientLegendary = true;
                plugin.PrimalAncientLegendary = true;
                // sets
                plugin.Set = true;
                plugin.AncientSet = true;
                plugin.PrimalAncientSet = true;

                // alerts when gambling ?
                plugin.Gambled = true;

                // ancient & primals prefixes
                plugin.AncientLegendaryNamePrefix = "Ancient";
                plugin.PrimalAncientLegendaryNamePrefix = "Primal";
                plugin.AncientSetNamePrefix = "Ancient";
                plugin.PrimalAncientSetNamePrefix = "Primal";

                // naming function
                // can be overriden with an anonymous function (item) => { return "string"; }
                //plugin.NameFunc = GetItemName;

                // Exceptions on above rules :
                // ---------------------------

                // add any item
                plugin.ItemSnos.Add(1844495708); // 1844495708 - Ramaladni's Gift
                //plugin.ItemSnos.Add(2332226049); // health globe

                // ancients items if ancient rank == 1 is not activated
                // example for // 916911834 - Sacred Harvester
                //plugin.AncientItemSnos.Add(916911834);

                // primals items if ancient rank == 2 is not activated
                //plugin.PrimalAncientItemSnos.Add(12354689);

                // custom items names (if the item is not in one of the other list, this will do nothing)
                plugin.ItemCustomNames.Add(1844495708, "OMAGAD a gift"); // 1844495708 - Ramaladni's Gift
                //plugin.ItemCustomNames.Add(2332226049, "health"); // health globe
            });

            //Hud.RunOnPlugin<MonsterRiftProgressionColoringPlugin>(plugin =>
            //{
            //    plugin.Decorator1.Add(new SoundAlertDecorator(Hud));
            //    plugin.Decorator2.Add(new SoundAlertDecorator(Hud));
            //    plugin.Decorator3.Add(new SoundAlertDecorator(Hud));
            //    plugin.Decorator4.Add(new SoundAlertDecorator(Hud));
            //    plugin.Decorator5.Add(new SoundAlertDecorator(Hud));
            //});

            Enabled = false;
        }
    }
}