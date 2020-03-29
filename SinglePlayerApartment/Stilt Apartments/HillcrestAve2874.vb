﻿Imports System.Drawing
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports System.Windows.Forms
Imports SinglePlayerApartment.SinglePlayerApartment
Imports INMNativeUI
Imports SinglePlayerApartment.Wardrobe
Imports SinglePlayerApartment.INMNative

Public Class HillcrestAve2874

    Public Shared _Apartment As Apartment
    Public Shared BuyMenu, ExitMenu, GarageMenu As UIMenu
    Public Shared _menuPool As MenuPool

    Public Sub New()
        Try
            _Apartment = New Apartment(Game.GetGXTEntry("MP_PROP_92"), 571000, Game.GetGXTEntry("MP_PROP_92DES"))
            _Apartment.Owner = Apt2874HillcrestOwner
            _Apartment.Entrance = New Vector3(-853.075, 695.4132, 148.7877)
            _Apartment.Save = New Vector3(-851.2404, 677.0281, 149.0784)
            _Apartment.TeleportInside = New Vector3(-859.5645, 688.7182, 152.8571)
            _Apartment.TeleportOutside = New Vector3(-853.2899, 698.7006, 148.7756)
            _Apartment.ApartmentExit = New Vector3(-859.9158, 691.5079, 152.8589)
            _Apartment.Wardrobe = New Vector3(-855.3519, 680.0969, 149.0531)
            _Apartment.GarageEntrance = New Vector3(-864.5076, 698.6345, 148.6063)
            _Apartment.GarageOutside = New Vector3(-862.7094, 700.4839, 148.595)
            _Apartment.GarageOutHeading = 328.02
            _Apartment.CameraPosition = New Vector3(-863.697, 713.9671, 152.9681)
            _Apartment.CameraRotation = New Vector3(-8.148409, 1.0781, -167.5327)
            _Apartment.CameraFOV = 50.0
            _Apartment.WardrobeHeading = 182.5082
            _Apartment.IsAtHome = False
            _Apartment.GaragePath = "scripts\SinglePlayerApartment\Garage\2874_hillcreast_ave\"
            _Apartment.PlayerMap = "HillcrestA2874"
            _Apartment.IPL = "apa_stilt_ch2_09b_ext2"
            _Apartment.Enabled = True
            _Apartment.InteriorID = Apartment.GetInteriorID(_Apartment.TeleportInside)
            If Not _Apartment.InteriorID = 0 Then InteriorIDList.Add(_Apartment.InteriorID)

            If Apt2874Hillcrest Then
                _menuPool = New MenuPool()
                CreateBuyMenu()
                CreateExitMenu()
                CreateGarageMenu()

                AddHandler BuyMenu.OnMenuClose, AddressOf MenuCloseHandler
                AddHandler ExitMenu.OnMenuClose, AddressOf MenuCloseHandler
                AddHandler BuyMenu.OnItemSelect, AddressOf BuyItemSelectHandler
                AddHandler ExitMenu.OnItemSelect, AddressOf ItemSelectHandler
                AddHandler GarageMenu.OnItemSelect, AddressOf GarageItemSelectHandler
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBuyMenu()
        Try
            BuyMenu = New UIMenu("", AptOptions, New Point(0, -107))
            Dim Rectangle = New UIResRectangle()
            Rectangle.Color = Color.FromArgb(0, 0, 0, 0)
            BuyMenu.SetBannerType(Rectangle)
            _menuPool.Add(BuyMenu)
            Dim item As New UIMenuItem(_Apartment.Name, _Apartment.Description)
            With item
                If _Apartment.Owner = Owner.Michael Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Michael)
                ElseIf _Apartment.Owner = Owner.Franklin Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Franklin)
                ElseIf _Apartment.Owner = Owner.Trevor Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Trevor)
                ElseIf _Apartment.Owner = Owner.Player3 Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Heart)
                Else
                    .SetRightLabel("$" & _Apartment.Cost.ToString("N"))
                    .SetRightBadge(UIMenuItem.BadgeStyle.None)
                End If
            End With
            BuyMenu.AddItem(item)
            BuyMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshMenu()
        BuyMenu.MenuItems.Clear()
        Dim item As New UIMenuItem(_Apartment.Name, _Apartment.Description)
        With item
            If _Apartment.Owner = Owner.Michael Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Michael)
            ElseIf _Apartment.Owner = Owner.Franklin Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Franklin)
            ElseIf _Apartment.Owner = Owner.Trevor Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Trevor)
            ElseIf _Apartment.Owner = Owner.Player3 Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Heart)
            Else
                .SetRightLabel("$" & _Apartment.Cost.ToString("N"))
                .SetRightBadge(UIMenuItem.BadgeStyle.None)
            End If
        End With
        BuyMenu.AddItem(item)
        BuyMenu.RefreshIndex()
    End Sub

    Public Shared Sub RefreshGarageMenu()
        GarageMenu.MenuItems.Clear()
        Dim item As New UIMenuItem(_Apartment.Name & Garage)
        With item
            If _Apartment.Owner = Owner.Michael Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Michael)
            ElseIf _Apartment.Owner = Owner.Franklin Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Franklin)
            ElseIf _Apartment.Owner = Owner.Trevor Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Trevor)
            ElseIf _Apartment.Owner = Owner.Player3 Then
                .SetRightBadge(UIMenuItem.BadgeStyle.Heart)
            Else
                .SetRightBadge(UIMenuItem.BadgeStyle.None)
            End If
        End With
        GarageMenu.AddItem(item)
        GarageMenu.RefreshIndex()
    End Sub

    Public Shared Sub CreateExitMenu()
        Try
            ExitMenu = New UIMenu("", AptOptions, New Point(0, -107))
            Dim Rectangle = New UIResRectangle()
            Rectangle.Color = Color.FromArgb(0, 0, 0, 0)
            ExitMenu.SetBannerType(Rectangle)
            _menuPool.Add(ExitMenu)
            ExitMenu.AddItem(New UIMenuItem(ExitApt))
            ExitMenu.AddItem(New UIMenuItem(EnterGarage))
            ExitMenu.AddItem(New UIMenuItem(SellApt))
            ExitMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateGarageMenu()
        Try
            GarageMenu = New UIMenu("", GrgOptions, New Point(0, -107))
            Dim Rectangle = New UIResRectangle()
            Rectangle.Color = Color.FromArgb(0, 0, 0, 0)
            GarageMenu.SetBannerType(Rectangle)
            _menuPool.Add(GarageMenu)
            Dim item As New UIMenuItem(_Apartment.Name & Garage)
            With item
                If _Apartment.Owner = Owner.Michael Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Michael)
                ElseIf _Apartment.Owner = Owner.Franklin Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Franklin)
                ElseIf _Apartment.Owner = Owner.Trevor Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Trevor)
                ElseIf _Apartment.Owner = Owner.Player3 Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Heart)
                Else
                    .SetRightBadge(UIMenuItem.BadgeStyle.None)
                End If
            End With
            GarageMenu.AddItem(item)
            GarageMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateHillcrestAve2874()
        _Apartment.CreateStilt(_Apartment)
    End Sub

    Public Sub MenuCloseHandler(sender As UIMenu)
        Try
            hideHud = False
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = ExitApt Then
                'Exit Apt
                ExitMenu.Visible = False
                Game.FadeScreenOut(500)
                Wait(500)
                Brain.TVOn = False
                Game.Player.Character.Position = _Apartment.TeleportOutside
                Wait(500)
                Game.FadeScreenIn(500)

                RemoveIPL(_Apartment.IPL)
            ElseIf selectedItem.Text = SellApt Then
                'Sell Apt
                ExitMenu.Visible = False
                Apt2874HillcrestOwner = UpdateValue(Of Owner)(owners, hillcrest2874, Owner.None)
                SavePosition2()
                Game.FadeScreenOut(500)
                Wait(500)
                SinglePlayerApartment.player.Money = (playerCash + _Apartment.Cost)
                _Apartment.Owner = Owner.None
                _Apartment.AptBlip.Remove()
                If Not _Apartment.GrgBlip Is Nothing Then _Apartment.GrgBlip.Remove()
                CreateHillcrestAve2874()
                Brain.TVOn = False
                Game.Player.Character.Position = _Apartment.TeleportOutside
                Wait(500)
                Game.FadeScreenIn(500)
                RefreshMenu()
                RefreshGarageMenu()

                RemoveIPL(_Apartment.IPL)
            ElseIf selectedItem.Text = EnterGarage Then
                'Enter Garage
                Game.FadeScreenOut(500)
                Wait(500)
                SetInteriorActive2(222.592, -968.1, -99) '10 car garage
                Brain.TVOn = False

                TenCarGarage.LastLocationName = _Apartment.Name
                TenCarGarage.lastLocationVector = _Apartment.ApartmentExit
                TenCarGarage.lastLocationGarageVector = _Apartment.GarageEntrance
                TenCarGarage.lastLocationGarageOutVector = _Apartment.GarageOutside
                TenCarGarage.lastLocationGarageOutHeading = _Apartment.GarageOutHeading
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                TenCarGarage.CurrentPath = _Apartment.GaragePath
                playerPed.Position = TenCarGarage.Elevator
                ExitMenu.Visible = False
                Wait(500)
                Game.FadeScreenIn(500)
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub BuyItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = _Apartment.Name AndAlso selectedItem.RightBadge = UIMenuItem.BadgeStyle.None AndAlso selectedItem.RightLabel = "$" & _Apartment.Cost.ToString("N") AndAlso _Apartment.Owner = Owner.None Then
                'Buy _Apartment
                If playerCash > _Apartment.Cost Then
                    Apt2874HillcrestOwner = UpdateValue(Of Owner)(owners, hillcrest2874, GetOwner)
                    Game.FadeScreenOut(500)
                    Wait(500)
                    If Website.freeRealEstate = False Then SinglePlayerApartment.player.Money = (playerCash - _Apartment.Cost)
                    _Apartment.Owner = GetOwner()
                    _Apartment.AptBlip.Remove()
                    If Not _Apartment.GrgBlip Is Nothing Then _Apartment.GrgBlip.Remove()
                    CreateHillcrestAve2874()
                    RefreshGarageMenu()
                    Mechanic.CreateMechanicMenu()
                    Wait(500)
                    Game.FadeScreenIn(500)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "PROPERTY_PURCHASE", "HUD_AWARDS", False)
                    BigMessageThread.MessageInstance.ShowWeaponPurchasedMessage("~y~" & PropPurchased, "~w~" & _Apartment.Name, Nothing)
                    If GetPlayerName() = "Michael" Then
                        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Michael)
                    ElseIf GetPlayerName() = "Franklin" Then
                        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Franklin)
                    ElseIf GetPlayerName() = "Trevor" Then
                        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Trevor)
                    ElseIf GetPlayerName() = "Player3" Then
                        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Heart)
                    End If
                    selectedItem.SetRightLabel("")
                Else
                    If GetPlayerName() = "Michael" Then
                        DisplayNotificationThisFrame(Maze, "", InsFundApartment, "CHAR_BANK_MAZE", True, IconType.RightJumpingArrow)
                    ElseIf GetPlayerName() = "Franklin" Then
                        DisplayNotificationThisFrame(Fleeca, "", InsFundApartment, "CHAR_BANK_FLEECA", True, IconType.RightJumpingArrow)
                    ElseIf GetPlayerName() = "Trevor" Then
                        DisplayNotificationThisFrame(BOL, "", InsFundApartment, "CHAR_BANK_BOL", True, IconType.RightJumpingArrow)
                    ElseIf GetPlayerName() = "Player3" Then
                        DisplayNotificationThisFrame(Maze, "", InsFundApartment, "CHAR_BANK_MAZE", True, IconType.RightJumpingArrow)
                    End If
                End If
            ElseIf selectedItem.Text = _Apartment.Name AndAlso Not selectedItem.RightBadge = UIMenuItem.BadgeStyle.None AndAlso _Apartment.Owner = GetOwner() Then
                'Enter _Apartment
                BuyMenu.Visible = False
                hideHud = False
                World.DestroyAllCameras()
                World.RenderingCamera = Nothing

                ToggleIPL(_Apartment.IPL)
                _Apartment.SetInteriorActive()
                Game.FadeScreenOut(500)
                Wait(500)
                Game.Player.Character.Position = _Apartment.TeleportInside
                If Website.merryChristmas Then ShowXmasTree(New Vector3(-859.7589, 682.8218, 152.6529))
                Wait(500)
                Game.FadeScreenIn(500)
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub GarageItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = _Apartment.Name & Garage AndAlso Not selectedItem.RightBadge = UIMenuItem.BadgeStyle.None AndAlso Not playerPed.IsInVehicle Then
            'Teleport to Garage

            ToggleIPL(_Apartment.IPL)

            Game.FadeScreenOut(500)
            Wait(500)
            SetInteriorActive2(222.592, -968.1, -99) '10 car garage
            _Apartment.SetInteriorActive()
            TenCarGarage.LastLocationName = _Apartment.Name
            TenCarGarage.lastLocationVector = _Apartment.ApartmentExit
            TenCarGarage.lastLocationGarageVector = _Apartment.GarageEntrance
            TenCarGarage.lastLocationGarageOutVector = _Apartment.GarageOutside
            TenCarGarage.lastLocationGarageOutHeading = _Apartment.GarageOutHeading
            TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
            TenCarGarage.CurrentPath = _Apartment.GaragePath
            playerPed.Position = TenCarGarage.GarageDoorL
            GarageMenu.Visible = False
            Wait(500)
            Game.FadeScreenIn(500)
        ElseIf selectedItem.Text = _Apartment.Name & Garage AndAlso Not selectedItem.RightBadge = UIMenuItem.BadgeStyle.None AndAlso playerPed.IsInVehicle Then
            On Error Resume Next
            Dim VehPlate0, VehPlate1, VehPlate2, VehPlate3, VehPlate4, VehPlate5, VehPlate6, VehPlate7, VehPlate8, VehPlate9 As String
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_0.cfg") Then VehPlate0 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_0.cfg") Else VehPlate0 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_1.cfg") Then VehPlate1 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_1.cfg") Else VehPlate1 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_2.cfg") Then VehPlate2 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_2.cfg") Else VehPlate2 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_3.cfg") Then VehPlate3 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_3.cfg") Else VehPlate3 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_4.cfg") Then VehPlate4 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_4.cfg") Else VehPlate4 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_5.cfg") Then VehPlate5 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_5.cfg") Else VehPlate5 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_6.cfg") Then VehPlate6 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_6.cfg") Else VehPlate6 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_7.cfg") Then VehPlate7 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_7.cfg") Else VehPlate7 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_8.cfg") Then VehPlate8 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_8.cfg") Else VehPlate8 = "0"
            If IO.File.Exists(_Apartment.GaragePath & "vehicle_9.cfg") Then VehPlate9 = ReadCfgValue("PlateNumber", _Apartment.GaragePath & "vehicle_9.cfg") Else VehPlate9 = "0"


            ToggleIPL(_Apartment.IPL)

            SetInteriorActive2(222.592, -968.1, -99) '10 car garage
            _Apartment.SetInteriorActive()
            TenCarGarage.CurrentPath = _Apartment.GaragePath
            TenCarGarage.LastLocationName = _Apartment.Name
            TenCarGarage.lastLocationVector = _Apartment.ApartmentExit
            TenCarGarage.lastLocationGarageVector = _Apartment.GarageEntrance
            TenCarGarage.lastLocationGarageOutVector = _Apartment.GarageOutside
            TenCarGarage.lastLocationGarageOutHeading = _Apartment.GarageOutHeading
            GarageMenu.Visible = False

            If playerPed.CurrentVehicle.NumberPlate = VehPlate0 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_0.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh0, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate1 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_1.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh1, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate2 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_2.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh2, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate3 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_3.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh3, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate4 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_4.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh4, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate5 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_5.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh5, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate6 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_6.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh6, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate7 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_7.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh7, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate8 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_8.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh8, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            ElseIf playerPed.CurrentVehicle.NumberPlate = VehPlate9 Then
                Game.FadeScreenOut(500)
                Wait(500)
                TenCarGarage.UpdateGarageVehicle(_Apartment.GaragePath & "vehicle_9.cfg", "False")
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                playerPed.CurrentVehicle.Delete()
                playerPed.Position = TenCarGarage.GarageDoorL
                SetIntoVehicle(playerPed, TenCarGarage.veh9, VehicleSeat.Driver)
                playerPed.Task.LeaveVehicle(playerPed.CurrentVehicle, True)
                Wait(500)
                Game.FadeScreenIn(500)
            Else
                TenCarGarage.LoadGarageVechicles(_Apartment.GaragePath)
                TenCarGarage.SaveGarageVehicle(_Apartment.GaragePath)
            End If
        End If
    End Sub

    Public Sub OnTick()
        Try
            If Not Game.IsLoading Then
                If Apt2874Hillcrest Then
                    'Enter _Apartment
                    If (Not BuyMenu.Visible AndAlso Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead) AndAlso _Apartment.EntranceDistance < 3.0 Then
                        DisplayHelpTextThisFrame(EnterApartment & _Apartment.Name)
                        If Game.IsControlJustPressed(0, GTA.Control.Context) Then
                            Game.FadeScreenOut(500)
                            Wait(500)
                            BuyMenu.Visible = True
                            World.RenderingCamera = World.CreateCamera(_Apartment.CameraPosition, _Apartment.CameraRotation, _Apartment.CameraFOV)
                            hideHud = True
                            Wait(500)
                            Game.FadeScreenIn(500)
                        End If
                    End If

                    'Save Game
                    If ((Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead) AndAlso _Apartment.Owner = GetOwner()) AndAlso _Apartment.SaveDistance < 3.0 Then
                        DisplayHelpTextThisFrame(SaveGame)
                        If Game.IsControlJustPressed(0, GTA.Control.Context) Then
                            playerMap = _Apartment.PlayerMap
                            Game.FadeScreenOut(500)
                            Wait(500)
                            TimeLapse(8)
                            Game.ShowSaveMenu()
                            SavePosition()
                            Wait(500)
                            Game.FadeScreenIn(500)
                        End If
                    End If

                    'Exit _Apartment
                    If ((Not ExitMenu.Visible AndAlso Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead) AndAlso _Apartment.Owner = GetOwner()) AndAlso _Apartment.ExitDistance < 2.0 Then
                        DisplayHelpTextThisFrame(ExitApartment & _Apartment.Name)
                        If Game.IsControlJustPressed(0, GTA.Control.Context) Then
                            ExitMenu.Visible = True
                        End If
                    End If

                    'Wardrobe
                    If ((WardrobeScriptStatus = -1) AndAlso (Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead) AndAlso _Apartment.Owner = GetOwner()) AndAlso _Apartment.WardrobeDistance < 1.0 Then
                        DisplayHelpTextThisFrame(ChangeClothes)
                        If Game.IsControlJustPressed(0, GTA.Control.Context) Then
                            WardrobeVector = _Apartment.Wardrobe
                            WardrobeHead = _Apartment.WardrobeHeading
                            WardrobeScriptStatus = 0
                            If GetPlayerName() = "Michael" Then
                                Player0W.Visible = True
                                MakeACamera()
                            ElseIf GetPlayerName() = "Franklin" Then
                                Player1W.Visible = True
                                MakeACamera()
                            ElseIf GetPlayerName() = “Trevor" Then
                                Player2W.Visible = True
                                MakeACamera()
                            ElseIf GetPlayerName() = "Player3" Then
                                If Game.Player.Character.Model.GetHashCode = 1885233650 Then
                                    Player3_MW.Visible = True
                                    MakeACamera()
                                ElseIf Game.Player.Character.Model.GetHashCode = -1667301416 Then
                                    Player3_FW.Visible = True
                                    MakeACamera()
                                End If
                            End If
                        End If
                    End If

                    'Enter Garage
                    If (Not playerPed.IsDead AndAlso _Apartment.Owner = GetOwner()) AndAlso _Apartment.GarageDistance < 5.0 Then
                        If Not playerPed.IsInVehicle AndAlso (Not GarageMenu.Visible) Then
                            DisplayHelpTextThisFrame(EnterApartmentHelp(Garage.Trim))
                            If Game.IsControlJustPressed(0, GTA.Control.Context) Then
                                GarageMenu.Visible = True
                            End If
                        ElseIf playerPed.IsInVehicle Then
                            If Resources.GetVehicleClass(playerPed.CurrentVehicle) = "Pegasus" Then
                                DisplayHelpTextThisFrame(CannotStore)
                            ElseIf playerPed.IsInVehicle AndAlso (Not GarageMenu.Visible) Then
                                DisplayHelpTextThisFrame(EnterApartmentHelp(Garage.Trim))
                                If Game.IsControlJustPressed(0, GTA.Control.Context) Then
                                    GarageMenu.Visible = True
                                End If
                            End If
                        End If
                    End If

                    'If playerInterior = _Apartment.InteriorID Then _Apartment.IsAtHome = True Else _Apartment.IsAtHome = False

                    Select Case playerInterior
                        Case _Apartment.InteriorID
                            _Apartment.IsAtHome = True
                            HIDE_MAP_OBJECT_THIS_FRAME()
                        Case Else
                            _Apartment.IsAtHome = False
                    End Select

                    If _Apartment.IsAtHome Then
                        HIDE_MAP_OBJECT_THIS_FRAME()
                    End If

                    _menuPool.ProcessMenus()
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub HIDE_MAP_OBJECT_THIS_FRAME()
        Native.Function.Call(Hash._0x4B5CFC83122DF602)
        Native.Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, "apa_ch2_09b_hs02"))
        Native.Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, "apa_ch2_09b_hs02b_details"))
        Native.Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, "apa_ch2_09b_Emissive_09_LOD"))
        Native.Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, "ch2_09b_botpoolHouse02_LOD"))
        Native.Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, "apa_ch2_09b_Emissive_09"))
        Native.Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, "apa_ch2_09b_hs02_balcony"))
        Native.Function.Call(Hash._0x3669F1B198DCAA4F)
    End Sub

    Public Sub OnAborted() 'Handles MyBase.Aborted
        Try
            If Not _Apartment.AptBlip Is Nothing Then _Apartment.AptBlip.Remove()
            If Not _Apartment.GrgBlip Is Nothing Then _Apartment.GrgBlip.Remove()
        Catch ex As Exception
        End Try
    End Sub
End Class
