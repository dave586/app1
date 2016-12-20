using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;




namespace customer_client
{
    public class menuItem
    {



        [PrimaryKey,AutoIncrement]


        public int ID { get; set; } // auto set when isnerted to the db
        
        public string foodName { get; set; }

        public string itemCost { get; set; }
        public int imageID { get; set; }


        public string foodDescription;
       // public decimal itemCost;
        private int itemNumber;//the number given to the item in the order
        public int itemStatus;
        
        
        /*
         * status code 0: not initialized
         * status code 1: ordered
         * status code 2: in Progress
         * status code 3: delivered to table
         * status code 4: canceled
         * */
        public int imageurl;// possible method to get pictures for food
        /*Features to add
         * -Add additional item costs (eg. add mushrooms and onions to a steak-> +0.80/possibly just add to item view
         * Array/arraylist of benefits such as vegan, gluten, ocean friendly etc
        */

        /*Constructors*/

        public menuItem() { } //needed for SQLite




        public menuItem(string name,string desc, string cost,int id ) {

            this.foodName = name;
            this.foodDescription = desc;
            this.itemCost = cost;
            this.imageID = id;




        }

        public menuItem(int itemNumber) //Get rid of after orderTableAct moved to newest constructor
        {
            this.itemNumber = itemNumber;
        }

        public menuItem(string itemName) //Get rid of after orderTableAct moved to newest constructor
        {
            this.foodName = itemName;
        }

  

        /*getters*/
        public string getFoodName()
        {
            return foodName;
        }
        public string getDescription()
        {
            return foodDescription;
        }
       




        public int getStatusCode()
        {
            return itemStatus;
        }
        public int getImgId() {

            
            return imageID;

        }


        



        /*setters*/
        public void setFoodName(String foodName)
        {
            foodName = this.foodName;
        }
        public void setDescription(string foodDescription)
        {
            foodDescription = this.foodDescription;
        }
     
        public void updateStatusCode(int newStatusCode)
        {
            itemStatus = newStatusCode;
        }
        public void setImgId(int id)
        {
            imageID = id;

           

        }




        public override string ToString() // called when object geven to list for default list display
        {
            return "this is "+foodName+": $"+itemCost;
        }






    }
}   