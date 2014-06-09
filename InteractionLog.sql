
select t2.type, details, date, touch_id, touch_x, touch_y
from Interaction_Log as t1 inner join Interaction_Type as t2 on t1.type = t2.id

