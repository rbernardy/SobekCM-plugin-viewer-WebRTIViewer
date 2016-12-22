
-- Add RTI as item view type
if ( not exists ( select 1 from SobekCM_Item_Viewer_Types where ViewType = 'RTI' ))
begin
	
	insert into SobekCM_Item_Viewer_Types ( ViewType, [Order], DefaultView, MenuOrder )
	values ( 'RTI', 1, 0, 100 );

end;
GO
