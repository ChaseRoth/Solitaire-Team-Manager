﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics.Drawables;
/*
* Project           : Solitaire-Team-Manager
*
* Author            : Chase Roth
* 
* Professor         : Brian Trager
* 
* Class             : NMAD.260.01 - Mobile App Development 1
*
* Purpose           : A Xamarin.Android mobile application that is based off trello. See readme @ 
*                       https://github.com/ChaseRoth/Solitaire-Team-Manager for me details.     
*/
namespace Solitaire
{   
    /// 
    ///     Title of the application was changed to have for relaitivity to what it actually does
    /// 
    [Activity(Label = "STM", MainLauncher = true)] 
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_activity);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "STM";
            SetSupportActionBar(toolbar);
            // Setting up a our hamburger menu
            
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hamburger);
            // Enabling it
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            // Default Startup Fragment - do this before the nav since the user will see this immediately, unlike the drawer nav
            Navigate(new ListBoardsFragment(this));

            // First we need to get a reference to our navigation view as a whole
            var navigationView = FindViewById<NavigationView>(Resource.Id.navigationMenu);
            navigationView.NavigationItemSelected += DrawerLayoutMenuItemSelected;
            // Then we get the header view from it
            var headerView = navigationView.GetHeaderView(0);
            // FINALLY at last we can get the references to the TextViews inside the headerView we want            
            // headerView.FindViewById<TextView>(Resource.Id.nameForNavHeader).Text = AssetManager.thisGoogleAccount.DisplayName;
            // headerView.FindViewById<TextView>(Resource.Id.emailForNavHeader).Text = AssetManager.thisGoogleAccount.Email;

            // Getting the avatar, using Refractored package for CircleImageView


            // headerView.FindViewById<Refractored.Controls.CircleImageView>(Resource.Id.avatarForNavHeader).SetImageDrawable(AssetManager.QueryGoogleAccountAvatar());

            //var test = AssetManager.thisGoogleAccount;

            // ClientManager.InitClientSocket();
        }
        
        /// 
        /// 
        ///     Replaces and commits new fragments to the FrameLayout for viewing
        /// 
        /// 
        void Navigate(Android.Support.V4.App.Fragment fragment)
        {            
            var transaction = base.SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment);            
            transaction.Commit();
        }

        /// 
        /// 
        ///     Will navigate our user to different primary activites of this app
        /// 
        /// 
        private void DrawerLayoutMenuItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            // Setting the checked to true so the user can see which item is currently selected
            e.MenuItem.SetChecked(true);
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.allContributors:
                    Navigate(new ListContributorsFragment(this));
                    break;
                case Resource.Id.allBoards:                   
                    Navigate(new ListBoardsFragment(this));
                    break;               
            }
            drawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
        }

        /// 
        /// 
        ///     Creating the menu
        /// 
        ///
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.navigation_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// 
        /// 
        ///     Handles Toolbar itemclick events
        /// 
        /// 
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    break;
                case Resource.Id.allContributors:
                    Navigate(new ListContributorsFragment(this));
                    break;
                case Resource.Id.allBoards:
                    Navigate(new ListBoardsFragment(this));
                    break;

            }
            return true;
        }      
    }
}