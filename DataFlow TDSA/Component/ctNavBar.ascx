<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctNavBar.ascx.cs" Inherits="DataFlow_TDSA.Component.WebUserControl1" %>
 <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

     <a class="sidebar-brand d-flex align-items-center justify-content-center" href="index.html">
         <div class="sidebar-brand-icon rotate-n-15">
             <i class="fas fa-laugh-wink"></i>
         </div>
         <div class="sidebar-brand-text mx-3">DataFlow TDSA</div>
     </a>

     <hr class="sidebar-divider my-0">

     <li class="nav-item">
         <a class="nav-link" href="../Table.aspx">
             <i class="fas fa-fw fa-table"></i>
             <span>Tabela</span></a>
     </li>

     <hr class="sidebar-divider d-none d-md-block">

     <div class="text-center d-none d-md-inline">
         <button type="button" class="rounded-circle border-0" id="sidebarToggle"></button>
     </div>
 </ul>