using System;

namespace finalProject {

    //Player class that stores player info.
    class Player {
        public string pName;
        public int wins;
        public int losses;
        public int ties;

        //Constructor.
        public Player (string pName, int wins, int losses, int ties){
            this.pName = pName;
            this.wins = wins;
            this.losses = losses;
            this.ties = ties;
        }

        public void toString(){
            //Console.WriteLine($"{this.pName} has {this.wins} wins, {this.losses} losses, and {this.ties} ties.");
            Console.WriteLine($"{this.pName}, {this.wins}, {this.losses}, {this.ties}");
        }

        //Helper to calculate round of player.
        public int getRound (){
            int round = 0;
            round = this.wins + this.losses + this.ties + 1;
            return round;
        }
    }
}