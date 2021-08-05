import { CallsPage } from './pages/CallsPage'
import { CallPage } from './pages/CallPage'
import { CreateCallPage } from './pages/CreateCallPage'
import { CategoriesPage } from './pages/CategoriesPage'
import { SettingsPage } from './pages/SettingsPage'
import { ExecutableCallsPage } from './pages/ExecutableCallsPage'
import { faCheck, faBars, faUsersCog, faPlus, } from '@fortawesome/fontawesome-free-solid'
import { faHeadset } from '@fortawesome/free-solid-svg-icons'


export const user = {
  firstName: "ზურაბ",
  lastName: "რევაზიშვილი",
  privateNumber: "00000000001",
  resources: "ROLE.ADMIN",
  userName: "admin"
}

export const routes = [
  {
    id: 1,
    path: '/calls',
    component: CallsPage,
    exact: true,
    name: 'ყველა ზარი',
    icon: faHeadset,
    permissons: ['ROLE.ADMIN', 'ROLE.SUPERVAISER', 'ROLE.OPERATOR']
  },
  {
    id: 2,
    path: '/calls/:id',
    component: CallPage,
    exact: true,
    permissons: ['ROLE.ADMIN', 'ROLE.SUPERVAISER', 'ROLE.OPERATOR']
  },
  {
    id: 3,
    path: '/createCall',
    component: CreateCallPage,
    exact: true,
    name: 'ზარის დამატება',
    icon: faPlus,
    permissons: ['ROLE.ADMIN', 'ROLE.OPERATOR']
  },
  {
    id: 4,
    path: '/executableCalls',
    component: ExecutableCallsPage,
    exact: true,
    name: 'შესასრულებელი',
    icon: faCheck,
    permissons: ['ROLE.ADMIN', 'ROLE.SUPERVAISER']
  },
  {
    id: 5,
    path: '/categories',
    component: CategoriesPage,
    exact: true,
    name: 'კატეგორიები',
    icon: faBars,
    permissons: ['ROLE.ADMIN']
  },
  {
    id: 6,
    path: '/settings',
    component: SettingsPage,
    exact: true,
    name: 'თვისებები',
    icon: faUsersCog,
    permissons: ['ROLE.ADMIN']
  },
]
