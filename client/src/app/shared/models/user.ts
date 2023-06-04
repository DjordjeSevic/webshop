export interface User {
    email: string;
    displayName: string;
    firstName: string;
    lastName: string;
    token: string;
    role: string;
}

export interface Address {
    street: string;
    city: string;
    state: string;
    zipCode: string;
}