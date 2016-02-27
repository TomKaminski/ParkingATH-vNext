INSERT INTO dbo.[User] (Charges, Email, IsAdmin, IsDeleted,Name, PasswordHash, PasswordSalt, UserPreferencesId, LockedOut, UnsuccessfulLoginAttempts)
VALUES (99999, 'system.administrator@smartpark.pl', 1, 0, 'Admin','MqQyUuIjwtigVQE6/mi41+os9ClOi9iDxwD99OuEg0ABYay6im/a2wFaUEjkNsNm','qT/wFYOS1NEj4rBERnPE5KuJ0SMYvjW2KVZY9CwbT0jcHp95XWyyUc6CmaxzB4T6',1, 0, 0)

INSERT INTO dbo.[UserPreferences] (ShrinkedSidebar, UserId)
VALUES (0, 1)

GO