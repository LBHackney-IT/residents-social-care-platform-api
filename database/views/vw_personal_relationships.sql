CREATE VIEW dbo.vw_personal_relationships AS
SELECT
  dpr.personal_relationship_id,
  dpr.person_id,
  dpr.other_person_id,
  dprt.description,
  CASE
    WHEN dprt.family_category = 'Child''s Children' THEN 'children'
    WHEN dprt.family_category = 'Child''s Siblings' THEN 'siblings'
    WHEN dprt.family_category = 'Child''s Parents' THEN 'parents'
    WHEN dprt.family_category = 'Other Family Relationships' THEN 'other'
    WHEN dprt.family_category IS NULL THEN 'other'
    ELSE 'unknown'
  END AS category
FROM
  dbo.dm_personal_relationships dpr
  INNER JOIN dbo.dm_personal_rel_types dprt ON dpr.personal_rel_type_id = dprt.personal_rel_type_id;
