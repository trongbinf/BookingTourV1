export interface ChangePassword {
    email: string;
    oldPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}