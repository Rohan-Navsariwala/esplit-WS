alter PROCEDURE GetContacts
    @UserID INT,
	@ContactStatus varchar(10)
AS
BEGIN
    SELECT	c.ContactID,
			c.ContactStatus,
			c.ConnectionInit,
			c.ApprovedOn,
			c.UserID1,
			c.UserID2,
			u.UserID,
			u.UserName,
			u.FullName,
			u.CreatedAt,
			u.isActive
    FROM dbo.Contacts c
    INNER JOIN dbo.Users u
        ON (u.UserID = c.UserID1 AND c.UserID2 = @UserID)
        OR (u.UserID = c.UserID2 AND c.UserID1 = @UserID)
	WHERE u.isActive=1 and c.ContactStatus = @ContactStatus
END
