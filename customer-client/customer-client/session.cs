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

namespace customer_client

{
    class session
    {
        public int tableID;
        public order orderHistory;//array of ordered stuff, type order
        public order pendingOrders;//array of order not yet complete, type order
        public customer customerList;// array of customer, type customer




        public session(string userName, int Password, int tableID) {
             
           orderHistory = new order();// array list of order
            pendingOrders = new order();
           customerList=new customer(userName, Password);
            this.tableID = tableID;

           // return sessionID;
           //need to look into the reason for returning a session id, might have redundency in checking stuff
        }

        public int addUser(string userName, int Password, int tableID) {

            customerList.add(userName, Password);
            //add a customer by username and password
            //this need to be checked with group member

        }

        public int fillOrder(int orderID) {
            //add and remove
            pendingOrders.remove(orderID);
            orderHistory.add(orderID);


        }
        public int kickUser(string userName, int passWord, string targetUserName) {

            //logic check username and pasword
            // simple two stage check, will be improved later on
            if (userName = owner.getUserName())
            {

                if (passWord = owner.getPass())
                {

                    customerList.remove(targetUserName);
                    //remove the user


                }
                else
                {


                    return 0;

                }





            }
            else { return 1; }


            //using 1 and 0 to retrun error message

       




        }

        public int checkOut(string userName, int passWord) {

            if (customerList.contains(userName)) {

                if (customerList.get(userName).getPass = passWord) {

                    customerList.get(userName).getPrice();
                    //get price method should add all item price and return an int

                }




            }




        }



        public int close(string userName, int passWord) {

                var total=0;
            foreach (customerList) {

                total = total + customerList.get(userName).getPrice();


            }
         
            // getting all total
        }





    }
}
