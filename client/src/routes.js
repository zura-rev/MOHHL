import { CallsPage } from './pages/CallsPage'
import { CallPage } from './pages/CallPage'
import { CreateCallPage } from './pages/CreateCallPage'
import { CategoriesPage } from './pages/CategoriesPage'
import { SettingsPage } from './pages/SettingsPage'
import { TasksPage } from './pages/TasksPage'
import { faCheck, faBars, faUsersCog, faPlus, } from '@fortawesome/fontawesome-free-solid'
import { faHeadset } from '@fortawesome/free-solid-svg-icons'


// export const user = {
//   firstName: "ზურაბ",
//   lastName: "რევაზიშვილი",
//   privateNumber: "00000000001",
//   resources: "ROLE.ADMIN",
//   userName: "admin"
// }

export const routes = [
  {
    id: 1,
    path: '/calls',
    component: CallsPage,
    exact: 'exact',
    name: 'ყველა ზარი',
    icon: faHeadset,
    permissons: ['ROLE.ADMIN', 'ROLE.SUPERVAISER', 'ROLE.OPERATOR']
  },
  {
    id: 2,
    path: '/calls/:id',
    component: CallPage,
    exact: 'exact',
    permissons: ['ROLE.ADMIN', 'ROLE.SUPERVAISER', 'ROLE.OPERATOR']
  },
  {
    id: 3,
    path: '/createCall',
    component: CreateCallPage,
    exact: 'exact',
    name: 'ზარის დამატება',
    icon: faPlus,
    permissons: ['ROLE.ADMIN', 'ROLE.OPERATOR']
  },
  {
    id: 4,
    path: '/tasks',
    component: TasksPage,
    exact: 'exact',
    name: 'დავალებები',
    icon: faCheck,
    permissons: ['ROLE.ADMIN', 'ROLE.SUPERVAISER']
  },
  {
    id: 5,
    path: '/categories',
    component: CategoriesPage,
    exact: 'exact',
    name: 'კატეგორიები',
    icon: faBars,
    permissons: ['ROLE.ADMIN']
  },
  {
    id: 6,
    path: '/settings',
    component: SettingsPage,
    exact: 'exact',
    name: 'თვისებები',
    icon: faUsersCog,
    permissons: ['ROLE.ADMIN']
  },
]
